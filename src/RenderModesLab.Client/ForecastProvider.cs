namespace RenderModesLab.Client;

/// <summary>A value that is different every time it is generated, so re-fetching is visible.</summary>
public sealed record Forecast(string GeneratedOn, DateTimeOffset GeneratedAt, int TemperatureC);

public interface IForecastProvider
{
    Forecast GetForecast();
}

/// <summary>
/// Stands in for a database or API call. Reports where it ran, so the UI can show whether the
/// data came from the server prerender or was fetched again in the browser.
/// </summary>
public sealed class ForecastProvider(TimeProvider? time = null) : IForecastProvider
{
    private readonly TimeProvider _time = time ?? TimeProvider.System;

    public Forecast GetForecast() => new(
        OperatingSystem.IsBrowser() ? "WebAssembly (browser)" : "Server",
        _time.GetUtcNow(),
        Random.Shared.Next(18, 46));
}
