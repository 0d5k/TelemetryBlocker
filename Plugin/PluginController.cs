using BepInEx;
using TelemetryBlocker;

namespace TelemetryBlocker.Plugin;

[BepInPlugin("com.harmonyblocks.telemetryfilter", "TelemetryFilter", "1.0.0")]
public class PluginController : BaseUnityPlugin
{
	private void OnEnable()
	{
		PatchManager.ApplyHarmonyPatches();
	}

	private void OnDisable()
	{
		PatchManager.RemoveHarmonyPatches();
	}
}
