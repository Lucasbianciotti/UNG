using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Web;
using Web.Services;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjIyODQ1QDMyMzAyZTMxMmUzMEowOHlmaThqOFluUEhPUStpRkZNRGRZdFRYaVhQZUQ2M0NwSXVENU55ckE9");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSyncfusionBlazor();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IToastServices, ToastServices>();
builder.Services.AddSingleton<IGlobalConfiguration_Services, GlobalConfiguration_Services>();
builder.Services.AddSingleton<IGlobalElements_Services, GlobalElements_Services>();

await builder.Build().RunAsync();
