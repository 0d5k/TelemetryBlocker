using HarmonyLib;
using PlayFab;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabClientAPI), "ReportDeviceInfo")]
internal class StaticDeviceInfoPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
