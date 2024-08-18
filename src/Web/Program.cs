using Microsoft.IdentityModel.Logging;
using SimpleAtm.Infrastructure.Data;
using SimpleAtm.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
    IdentityModelEventSource.ShowPII = true;
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseSwaggerUi(settings =>
//{
//    settings.Path = "/api";
//    settings.DocumentPath = "/api/specification.json";
//});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action=Index}/{id?}");

//app.MapRazorPages();

//app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });


app.UseRouting();
//app.MapEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.Map("/", () => Results.Redirect("/graphql"));
app.MapGraphQL();


app.Run();

public partial class Program { }
