using SimpleAtm.Infrastructure.Data;
using SimpleAtm.Web.Schema.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the containe
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

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

//app.Map("/", () => Results.Redirect("/api"));

//app.MapEndpoints();

app.MapGraphQL();

app.Run();

public partial class Program { }
