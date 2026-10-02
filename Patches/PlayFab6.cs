using HarmonyLib;
using PlayFab;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabClientAPI), "AttributeInstall")]
internal class ClientAttributeInstallPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
