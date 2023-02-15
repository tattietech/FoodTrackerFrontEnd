global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using foodTrackerFrontEnd;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using foodTrackerFrontEnd.Interfaces;
using foodTrackerFrontEnd.Models;
using foodTrackerFrontEnd.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://1v9h259fs8.execute-api.eu-west-2.amazonaws.com") });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped<IFoodTrackerApiService<FoodItem>, FoodItemService>();
builder.Services.AddScoped<IFoodTrackerApiService<FoodStorage>, FoodStorageService>();
builder.Services.AddScoped<IFoodStorageService, FoodStorageService>();

await builder.Build().RunAsync();
