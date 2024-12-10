using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MusicTimeClient;
using MusicTimeClient.Contracts;
using MusicTimeClient.Services;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7207") });
//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();