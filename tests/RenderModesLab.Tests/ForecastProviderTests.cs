using RenderModesLab.Client;

namespace RenderModesLab.Tests;

public class ForecastProviderTests
{
    [Fact]
    public void Reports_server_when_not_running_in_a_browser()
    {
        var forecast = new ForecastProvider().GetForecast();

        Assert.Equal("Server", forecast.GeneratedOn);
        Assert.InRange(forecast.TemperatureC, 18, 45);
    }
}
