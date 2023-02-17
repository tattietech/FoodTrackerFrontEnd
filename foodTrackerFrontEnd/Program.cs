global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using foodTrackerFrontEnd;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using foodTrackerFrontEnd.Interfaces;
using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Services;
using MudBlazor.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://1v9h259fs8.execute-api.eu-west-2.amazonaws.com") });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<ISnackbar, SnackbarService>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddScoped<IFoodTrackerApiService<FoodItem>, FoodItemService>();
builder.Services.AddScoped<IFoodTrackerApiService<FoodStorage>, FoodStorageService>();
builder.Services.AddScoped<IApiAuthService, ApiAuthService>();
builder.Services.AddScoped<AppState>();

await builder.Build().RunAsync();
