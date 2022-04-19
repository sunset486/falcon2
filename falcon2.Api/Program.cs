global using Microsoft.EntityFrameworkCore;
global using falcon2.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using System.Reflection;
using falcon2.Core;
using falcon2.Core.Models.Auth;
using falcon2.Services;
using falcon2.Core.Services;
using falcon2.Api.Helpers;
using falcon2.Api.Settings;
using falcon2.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.

services.AddCors();
services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
services.AddDbContext<SuperDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"), x =>
    x.MigrationsAssembly("falcon2.Data")));
services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
    options.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<SuperDbContext>()
    .AddDefaultTokenProviders();
services.AddAutoMapper(typeof(Program));
services.Configure<JwtSettings>(config.GetSection("Jwt"));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddTransient<ISuperHeroService, SuperHeroService>();
services.AddTransient<ISuperPowerService, SuperPowerService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Super Hero DataBase", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT containing userid claim",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    var security = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                UnresolvedReference = true
            },
            new List<string>()
        }
    };
    options.AddSecurityRequirement(security);
});

var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>();
services.AddAuth(jwtSettings);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Super Hero DataBase v1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuth();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
