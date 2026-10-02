using HarmonyLib;
using PlayFab;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportDeviceInfo")]
internal class DeviceInfoPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
