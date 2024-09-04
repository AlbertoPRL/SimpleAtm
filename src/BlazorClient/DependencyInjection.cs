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
                try
                {
                    TokenService tokenService = serviceProvider.GetRequiredService<TokenService>();
                    var token = tokenService.GetToken().GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Add("Bearer", token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

        services.AddBlazoredSessionStorage();

        services.AddScoped<TokenService>();

        return services;
    }
}
