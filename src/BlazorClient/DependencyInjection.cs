using BlazorClient.Services;
using Blazored.SessionStorage;
using MudBlazor.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {

        services.AddMudServices();
        services.AddBankApiGraphqlClient()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                client.BaseAddress =
                     new Uri("https://localhost:7181/graphql/");
                TokenService tokenService = serviceProvider.GetRequiredService<TokenService>();
                client.DefaultRequestHeaders.Add("Bearer", tokenService.token);
            });

        services.AddBlazoredSessionStorage();

        services.AddScoped<TokenService>();

        return services;
    }
}
