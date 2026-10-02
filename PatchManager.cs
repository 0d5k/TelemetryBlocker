using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TelemetryBlocker;

public class PatchManager : MonoBehaviour
{
	public static Harmony instance;

	public static bool IsPatched { get; private set; }

	public static void ApplyHarmonyPatches()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected Obj, but got Unknown
		if (!IsPatched)
		{
			if (instance == null)
			{
				instance = new Harmony("com.harmonyblocks.telemetryfilter");
			}
			instance.PatchAll(Assembly.GetExecutingAssembly());
			IsPatched = true;
		}
	}

	public static void RemoveHarmonyPatches()
	{
		if (instance != null && IsPatched)
		{
			IsPatched = false;
		}
	}
}
