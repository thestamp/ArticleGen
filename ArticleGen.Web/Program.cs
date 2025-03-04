using ArticleGen.Core.Services;
using Common.OpenAiClient.Configuration;
using Common.Web;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
//settings
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Ensures that the app can find the appsettings.json file in the current directory
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables() // Adds environment variables to the configuration
    .AddUserSecrets<Program>()
    .Build();


builder.Services.AddScoped<GenArticleService>();
var gptClientSettings = configuration.BindAndAddSingleton<GptClientSettingsModel>(builder.Services, "GptClientSettings");
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
