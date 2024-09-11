using BlazorClient.Services;
using Blazored.SessionStorage;
using MudBlazor.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        services.AddSingleton<TokenStore>();

        services.AddBlazoredSessionStorage();
        services.AddMudServices();
        services.AddBankApiGraphqlClient()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var token = serviceProvider.GetRequiredService<TokenStore>().Token;

                client.BaseAddress =
                     new Uri("https://localhost:7181/graphql/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token ?? string.Empty}");
            });


        return services;
    }
}
