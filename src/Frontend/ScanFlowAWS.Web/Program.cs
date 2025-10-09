using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScanFlowAWS.Web;
using ScanFlowAWS.Web.Services;
using ScanFlowAWS.Web.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7208") });
builder.Services.AddScoped<ILoginService, LoginService>();



await builder.Build().RunAsync();
