using System;
using System.Linq;
using System.Security.Cryptography;
using HarmonyLib;

namespace TelemetryBlocker.Patches;

[HarmonyPatch(/*Could not decode attribute arguments.*/)]
public class HardwareIdPatch
{
	private static string CachedHardwareId = "";

	private static bool Prefix(ref string __result)
	{
		if (CachedHardwareId == "")
		{
			string text = string.Join("-", from b in GenerateRandomBytes(6)
				select $"{b:X2}");
			byte[] inArray = GenerateRandomBytes(3);
			string text2 = Convert.ToBase64String(inArray).Substring(0, 4).Replace("+", "G")
				.Replace("/", "H");
			string text3 = BitConverter.ToString(GenerateRandomBytes(4)).Replace("-", "");
			string text4 = BitConverter.ToString(GenerateRandomBytes(4)).Replace("-", "");
			CachedHardwareId = text + "-" + text2 + "-" + text3 + "-" + text4;
		}
		if (CachedHardwareId != "")
		{
			__result = CachedHardwareId;
		}
		else
		{
			__result = GenerateRandomHardwareId();
		}
		return false;
	}

	private static byte[] GenerateRandomBytes(int length)
	{
		byte[] array = new byte[length];
		using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
		{
			randomNumberGenerator.GetBytes(array);
		}
		return array;
	}

	private static string GenerateRandomHardwareId()
	{
		Random random = new Random();
        return new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 16)
			select s[random.Next(s.Length)]).ToArray());
	}
}
