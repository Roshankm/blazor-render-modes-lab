using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using RenderModesLab.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// The same service is registered on the server, so components work in every render mode.
builder.Services.AddSingleton<IForecastProvider, ForecastProvider>();

await builder.Build().RunAsync();
