using HarmonyLib;
using PlayFab.Internal;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabDeviceUtil), "DoAttributeInstall")]
internal class AttributeInstallPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
