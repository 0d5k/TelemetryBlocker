# TelemetryBlocker

A small BepInEx plugin for Unity games that blocks telemetry and device reporting on the client, using Harmony patches.

## What it does

TelemetryBlocker patches the game at runtime so these calls never run:

| Patch | Target | Effect |
| --- | --- | --- |
| `TelemetryInterceptor` | LIV LCK telemetry (`SendTelemetry`, `SendTelemetryAsync`, `Initialize`, `InitializeAsync`, `InitializeHttpClient`, `GetGeoLocation`, `LoadOrCreateDeviceId`, `SerializeTelemetryEvent`, `SetUserIdProvider`) | Skips the methods, so no telemetry events or geolocation lookups are sent |
| `DeviceInfoPatch` | `PlayFabClientInstanceAPI.ReportDeviceInfo` | Stops device info being reported to PlayFab |
| `ScreenTimeTrackerPatch` | `PlayFabHttp.InitializeScreenTimeTracker` | Disables PlayFab screen-time tracking |
| `HardwareIdPatch` | Hardware ID getter | Returns a random ID, generated once per session, instead of the real one |

Nothing is written to disk and nothing is sent anywhere. The plugin only skips existing calls.

## Requirements

- A Unity game with [BepInEx](https://github.com/BepInEx/BepInEx) installed
- The game's own `Assembly-CSharp`, `PlayFab` and `LIV.LCK` assemblies (only needed to build)

## Install

1. Build the project (see below) or download `TelemetryBlocker.dll` from Releases.
2. Copy `TelemetryBlocker.dll` into the game's `BepInEx/plugins/` folder.
3. Start the game. Patches are applied when the plugin is enabled.

## Build

1. Clone the repo.
2. Add references to `BepInEx`, `0Harmony`, `UnityEngine.CoreModule`, `Assembly-CSharp`, `PlayFab` and `LIV.LCK` (from the game's `Managed` folder and BepInEx's `core` folder).
3. **Remove or exclude the `Stubs/` folder.** It holds empty placeholder types so the project can compile without the game. If it is included, the plugin compiles against the placeholders instead of the real Harmony and patches will not be applied.
4. Build:

```
dotnet build -c Release
```

The output is `bin/Release/netstandard2.1/TelemetryBlocker.dll`.

## Project layout

```
TelemetryBlocker/
├── PatchManager.cs          # Creates the Harmony instance and applies patches
├── Plugin/
│   └── PluginController.cs  # BepInEx plugin entry point
├── Patches/
│   ├── PluginInfo.cs        # GUID, name, version
│   ├── LckTelemetryPatcher.cs
│   ├── HWIDPatch.cs
│   └── PlayFab1-8.cs        # PlayFab reporting patches
├── Stubs/                   # Compile-only placeholders (do not ship)
└── Decompile-Dll.ps1        # Helper script: decompile a DLL with ilspycmd
```

## Known issues

- `HardwareIdPatch` is missing its `[HarmonyPatch(...)]` target arguments (lost during decompilation), so it does nothing until they are restored.
- `RemoveHarmonyPatches` only resets a flag; it does not unpatch the game.

## Disclaimer

This is an unofficial project and is not affiliated with or endorsed by any game developer, PlayFab or LIV. Modifying a game client may break its terms of service. Use it at your own risk.

## License

Add a license of your choice (for example MIT) before publishing.
