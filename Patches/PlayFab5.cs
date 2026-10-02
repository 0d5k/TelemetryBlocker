using HarmonyLib;
using PlayFab.Internal;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabDeviceUtil), "SendDeviceInfoToPlayFab")]
internal class DeviceTelemetryPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
