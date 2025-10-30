using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScanFlowAWS.Web;
using ScanFlowAWS.Web.Services;
using ScanFlowAWS.Web.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = "https://localhost:7208/";

builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddScoped<TokenRefreshHandler>();

builder.Services.AddHttpClient("AuthorizedAPI", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<TokenRefreshHandler>();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAnalyzeImageService, AnalyzeImageService>();
builder.Services.AddScoped<ICompareImageService, CompareImageService>();

await builder.Build().RunAsync();
