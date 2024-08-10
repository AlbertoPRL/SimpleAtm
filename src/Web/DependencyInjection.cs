using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Infrastructure.Data;
using SimpleAtm.Web.Schema.Mutation;
using SimpleAtm.Web.Schema.Query;
using SimpleAtm.Web.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddGraphQLServer()
                // .AddDefaultTransactionScopeHandler()
                .RegisterDbContext<ApplicationDbContext>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddAuthorization()
                .AddProjections();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

       // services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

       // services.AddEndpointsApiExplorer();

        //services.AddOpenApiDocument((configure, sp) =>
        //{
        //    configure.Title = "SimpleAtm API";

        //    // Add JWT
        //    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        //    {
        //        Type = OpenApiSecuritySchemeType.ApiKey,
        //        Name = "Authorization",
        //        In = OpenApiSecurityApiKeyLocation.Header,
        //        Description = "Type into the textbox: Bearer {your JWT token}."
        //    });

        //    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        //});

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}

