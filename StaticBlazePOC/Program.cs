using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StaticBlazePOC;
using StaticBlazePOC.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
var patToken = builder.Configuration["GitHubPat"];


builder.Services.AddScoped(sp => new HttpClient { });

builder.Services.AddScoped<GitHubService>();

await builder.Build().RunAsync();