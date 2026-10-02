
using System;

namespace HarmonyLib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class HarmonyPatchAttribute : Attribute
    {
        public HarmonyPatchAttribute() { }
        public HarmonyPatchAttribute(Type type) { }
        public HarmonyPatchAttribute(Type type, string methodName) { }
        public HarmonyPatchAttribute(string methodName) { }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class HarmonyTargetMethodsAttribute : Attribute
    {
        public HarmonyTargetMethodsAttribute() { }
    }

    // Minimal Harmony class to allow field/usage in project code
    public class Harmony
    {
        public Harmony(string id) { }
        public void PatchAll(System.Reflection.Assembly assembly) { }
        public void UnpatchAll() { }
    }
}
