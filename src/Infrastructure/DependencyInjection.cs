using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleAtm.Application.Common.Exceptions;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Infrastructure.Data;
using SimpleAtm.Infrastructure.Data.Interceptors;
using SimpleAtm.Infrastructure.Data.Repositories;
using SimpleAtm.Infrastructure.Identity;
using SimpleAtm.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var secretKey = configuration["JwtSettings:SecretKey"];
        Console.WriteLine(secretKey);

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        //services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine);
        });

        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        //    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        //})
        //.AddJwtBearer(IdentityConstants.BearerScheme, options =>
        //{
        //    var jwtSettings = configuration.GetSection("JwtSettings");
        //    var secretKey = jwtSettings["SecretKey"];
        //    if(string.IsNullOrEmpty(secretKey))
        //    {
        //        throw new TokenConfigurationException("Secret key is missing.");
        //    }
        //    var key = Encoding.ASCII.GetBytes(secretKey);


        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = jwtSettings["Issuer"],
        //        ValidAudience = jwtSettings["Audience"],
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //    };
        //});
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            if (jwtSettings == null)
            {
                throw new TokenConfigurationException("JwtSettings section is missing.");
            }
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new TokenConfigurationException("Secret key is missing or invalid.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"]
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    context.Token = token;
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            //.AddSignInManager<SignInManager<ApplicationUser>>();
            /*.AddApiEndpoints()*/

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<LogInServices, LogInServices>();

        services.AddAuthorization();
            //options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
