# Blazor Render Modes Lab

[![CI](https://github.com/Roshankm/blazor-render-modes-lab/actions/workflows/ci.yml/badge.svg)](https://github.com/Roshankm/blazor-render-modes-lab/actions/workflows/ci.yml)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)

A small .NET 10 Blazor Web App for seeing **Static SSR, Interactive Server, Interactive WebAssembly and Interactive Auto** side by side, plus the prerendering and state problems that come with them.

Part of my set of Blazor reference labs, each one focused on a single feature with tests and CI.

## What's inside

| Page | Shows |
|---|---|
| **Compare modes** (`/`) | One `RenderModeProbe` component rendered four ways. Each card reports `RendererInfo.Name`, `RendererInfo.IsInteractive` and `AssignedRenderMode`, and its button stays disabled until the component is interactive |
| **Persistent state** (`/state`) | Prerendering runs `OnInitialized` twice. One panel loads data again in the browser (and flickers); the other uses .NET 10's `[PersistentState]` to reuse the prerendered value |
| **Streaming** (`/streaming`) | `[StreamRendering]` on a static page: the layout arrives immediately and the slow part streams into the same response |

## Run it

```bash
dotnet run --project src/RenderModesLab --launch-profile https
dotnet test
```

Open https://localhost:7301 and reload a page to watch components move from prerendered to interactive.

## Project layout

```
src/RenderModesLab          Server: hosting, static pages, Interactive Server
src/RenderModesLab.Client   WebAssembly: components that must also run in the browser
tests/RenderModesLab.Tests  Prerendered-HTML tests through WebApplicationFactory
```

Components used with `InteractiveWebAssembly` or `InteractiveAuto` live in the client project, because their code has to be downloaded to the browser. Services they use (`IForecastProvider`) are registered in **both** `Program.cs` files.

## Lessons this lab demonstrates

* Pick the render mode per component, not per app: most pages can stay static.
* Disable controls until `RendererInfo.IsInteractive` is true, so early clicks are not lost.
* Anything loaded in `OnInitialized` runs twice with prerendering. Persist it, or turn prerendering off for that component.
* `InteractiveAuto` starts on the server and switches to WebAssembly once the runtime is cached, so its code must work in both places.

## License

[MIT](LICENSE)
