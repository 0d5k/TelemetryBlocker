using HarmonyLib;
using PlayFab.Internal;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabHttp), "InitializeScreenTimeTracker")]
internal class ScreenTimeTrackerPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
