using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

namespace RenderModesLab.Tests;

/// <summary>
/// Requests pages through the real pipeline and checks the prerendered HTML: every interactive
/// component is rendered on the server first, and marked so the browser can take it over.
/// </summary>
public class PrerenderingTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Home_prerenders_all_four_render_modes()
    {
        var html = await factory.CreateClient().GetStringAsync("/");

        Assert.Contains("Static server rendering", html);
        Assert.Contains("InteractiveServer", html);
        Assert.Contains("InteractiveWebAssembly", html);
        Assert.Contains("InteractiveAuto", html);

        // Prerendered output is never interactive yet, so every button starts disabled.
        Assert.DoesNotContain("data-test=\"interactive\">Yes", html);
    }

    [Fact]
    public async Task Interactive_components_are_marked_for_the_browser_to_resume()
    {
        var html = await factory.CreateClient().GetStringAsync("/");

        Assert.Contains("<!--Blazor:", html);
    }

    [Fact]
    public async Task State_page_prerenders_both_panels_on_the_server()
    {
        var html = await factory.CreateClient().GetStringAsync("/state");

        Assert.Contains("Without persisted state", html);
        Assert.Contains("With [PersistentState]", html);
        Assert.Contains("Generated on <strong data-test=\"generated-on\">Server</strong>", html);
    }

    [Fact]
    public async Task Streaming_page_eventually_contains_the_loaded_data()
    {
        var response = await factory.CreateClient().GetAsync("/streaming");
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("data-test=\"loading\"", html);
        Assert.Contains("data-test=\"loaded\"", html);
    }
}
