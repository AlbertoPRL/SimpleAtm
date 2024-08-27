using Azure.Identity;
using MudBlazor.Services;
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
        services.AddBankApiGraphql()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7181/graphql"));
        services.AddMudServices();
        services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddProjections();

        services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

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

