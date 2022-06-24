using Admin;
using Admin.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("NjM0ODA2QDMyMzAyZTMxMmUzMEhEclYrbHNUSVBhWTBqbWRNL2N2ZkVGOHAwWTlVdVJvTm1ucGl6MGZYdzA9");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddSyncfusionBlazor();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ILocalStorage_Services, LocalStorage_Services>();

builder.Services.AddScoped<IAuthentication_Services, Authentication_Services>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProvider_Services>();

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IToast_Services, Toast_Services>();
builder.Services.AddSingleton<IGlobalConfiguration_Services, GlobalConfiguration_Services>();
builder.Services.AddScoped<IGlobalElements_Services, GlobalElements_Services>();

await builder.Build().RunAsync();
