using BBQ_Schedule.UI.Web;
using BBQ_Schedule.UI.Web.Services.Authentication;
using BBQ_Schedule.UI.Web.Services.EventScheduling;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>(
    p => p.GetRequiredService<AuthenticationProvider>());

builder.Services.AddScoped<IAuthorizationService, AuthenticationProvider>(
    p => p.GetRequiredService<AuthenticationProvider>());

builder.Services.AddScoped<IEventSchedulingService, EventSchedulingService>();

builder.Services.AddHttpClient("Schedule_Event", options =>
{
    options.BaseAddress = new Uri("https://localhost:44345");
    options.Timeout = TimeSpan.FromSeconds(10);
});

await builder.Build().RunAsync();
