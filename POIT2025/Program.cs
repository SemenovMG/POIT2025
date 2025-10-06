using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POIT2025.Data;
using POIT2025.Entities;
using POIT2025.Repositories;
using POIT2025.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<IGroupRepository, GroupRepository>();

services.AddScoped<IAccountService, AccountService>();
services.AddScoped<ITokenService, TokenService>();

services.AddScoped<IGroupService, GroupService>();

services.AddIdentity<UniversityUser, IdentityRole>()
    .AddEntityFrameworkStores<ScheduleContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(TokenService.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

services.AddAuthorization();

//DB 
var connString = "Server=(localdb)\\MSSQLLocalDB;Database=POIT2025;Trusted_Connection=True;";
builder.Services.AddDbContext<ScheduleContext>(
    options => options.UseSqlServer(connString));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


var app = builder.Build();


// Configure Middleware Pipeline

// Hidden exception handling
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
