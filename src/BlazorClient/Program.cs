using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddBlazorServices();
//builder.Services.AddScoped<ITokenService, TokenService>();

await builder.Build().RunAsync();
