using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StaticBlazePOC;
using StaticBlazePOC.Pages;
using StaticBlazePOC.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

SecretHolder.PatToken = Environment.GetEnvironmentVariable("GH_PAT");;

builder.Services.AddScoped(sp => new HttpClient
{
});

builder.Services.AddScoped<GitHubService>();


await builder.Build().RunAsync();

//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