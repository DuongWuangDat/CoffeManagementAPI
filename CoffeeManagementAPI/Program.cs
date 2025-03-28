using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Factory;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.Repository;
using CoffeeManagementAPI.Services;
using CoffeeManagementAPI.Strategy.SendVoucherStrategy;
using CoffeeManagementAPI.Strategy.StorageStrategy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffeemanagement API", Version = "v1", Description = "https://coffeemanagementapi.azurewebsites.net" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Type = SecuritySchemeType.Http,
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    }) ;

    option.OperationFilter<AddCharsetOperationFilter>();
});

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.DateFormatString = "dd/MM/yyyy";
});

//Prepare DB context
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectString"));
});


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme =
    option.DefaultScheme =
    option.DefaultForbidScheme =
    option.DefaultChallengeScheme =
    option.DefaultSignOutScheme =
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Aud"],
        
    };

    option.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            
            var err = new
            {
                message = "Authentication is failed",
                detail = context.Exception?.Message,
                statusCode = StatusCodes.Status401Unauthorized,

            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(err));
        },

        OnChallenge = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var err = new
            {
                message = "Authentication is failed",
                statusCode = StatusCodes.Status401Unauthorized,

            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(err));
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", (p) =>
    {
        p.WithOrigins("http://localhost:3000", "https://cafe.duong.website")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IAuthorization, AuthorizationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPayTypeRepository, PayTypeRepository>();
builder.Services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableTypeRepository, TableTypeRepository>();

//Strategy Pattern implements
builder.Services.AddTransient<CloudinaryStrategy>();
builder.Services.AddTransient<FirebaseStrategy>();
builder.Services.AddTransient<SendEmailStrategy>();
builder.Services.AddTransient<SendSMSStrategy>();

//Factory Pattern implements
builder.Services.AddSingleton<StorageFactory>();
builder.Services.AddSingleton<SendVoucherFactory>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = string.Empty;
    });
    app.UseExceptionHandler(appBuilder =>
    {

        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var err = new
            {
                message = "Something went wrong",
                statusCode = 500
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(err));
        });
    });
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
