using HarmonyLib;
using PlayFab.Internal;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabDeviceUtil), "GetAdvertIdFromUnity")]
internal class AdvertisementIdPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
