using HarmonyLib;
using PlayFab;
using UnityEngine;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportPlayer")]
internal class PlayerReportPatch : MonoBehaviour
{
	private static bool Prefix()
	{
		return false;
	}
}
