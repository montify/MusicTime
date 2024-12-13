using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MusicTimeClient;
using MusicTimeClient.Contracts;
using MusicTimeClient.Provider;
using MusicTimeClient.Services;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<LocalStorage>();
builder.Services.AddScoped<AuthHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

//https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-9.0
builder.Services.AddHttpClient("Serva",
        client => client.BaseAddress = new Uri("https://localhost:7207"))
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("Serva"));

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();