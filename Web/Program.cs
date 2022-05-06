using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Newtonsoft.Json;
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

builder.Services.AddScoped<ILocalStorage_Services, LocalStorage_Services>();

builder.Services.AddScoped<IAuthentication_Services, Authentication_Services>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProvider_Services>();

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IToast_Services, Toast_Services>();
builder.Services.AddSingleton<IGlobalConfiguration_Services, GlobalConfiguration_Services>();
builder.Services.AddScoped<IGlobalElements_Services, GlobalElements_Services>();


builder.Services.AddSingleton<HubConnection>(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri(URLs._API + URLs.ReportSignal))
      .WithAutomaticReconnect()
      .AddNewtonsoftJsonProtocol(opts => opts.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Auto)
      .Build();
});


await builder.Build().RunAsync();
