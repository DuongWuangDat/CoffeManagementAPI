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
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IAuthorization, AuthorizationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

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
