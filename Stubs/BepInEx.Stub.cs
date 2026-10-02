using System;

namespace BepInEx
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BepInPluginAttribute : Attribute
    {
        public BepInPluginAttribute(string guid, string name, string version) { }
    }

    public class BaseUnityPlugin : UnityEngine.MonoBehaviour { }
}
