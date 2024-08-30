using MudBlazor.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        services.AddMudServices();
        //services.AddBankApiGraphql()
        //    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7181/graphql"));

        return services;
    }
}
