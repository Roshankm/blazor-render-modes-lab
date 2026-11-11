using RenderModesLab.Client;
using RenderModesLab.Components;

var builder = WebApplication.CreateBuilder(args);

// Enable all three hosting models; each component picks its own render mode.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<IForecastProvider, ForecastProvider>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(RenderModesLab.Client._Imports).Assembly);

await app.RunAsync();

/// <summary>Exposed for <c>WebApplicationFactory</c> in the tests.</summary>
public partial class Program;
