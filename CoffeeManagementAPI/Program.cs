using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.Repository;
using CoffeeManagementAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Prepare DB context
builder.Services.AddDbContext<ApplicaitonDBContext>(option =>
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
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<ISendMailService, SendEmailService>();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
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

app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapControllers();

app.UseAuthentication();
app.Use(async (context, next) =>
{
    
    using(var scope = app.Services.CreateScope())
    {
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        
        var header = context.Request.Headers["Authorization"].FirstOrDefault();
        if(header == null)
        {
            await next.Invoke();
            return;
        }
        var token = header.Substring("Bearer ".Length).Trim();

        var checkToken = await tokenService.IsValidateToken(token);

        if (!checkToken)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message="Invalid token"
            }));

            return;
        }
    }


    await next.Invoke();
});

app.UseAuthorization();

app.Run();
