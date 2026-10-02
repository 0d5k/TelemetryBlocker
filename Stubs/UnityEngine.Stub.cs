using System;

namespace UnityEngine
{
    // Minimal stubs to allow compilation outside of Unity editor/runtime.
    public class MonoBehaviour { }
    public class GameObject { }
    public class Component { }
    public class Object { }
    public struct Vector3 { public float x, y, z; }
    public static class Debug
    {
        public static void Log(object message) { }
        public static void LogWarning(object message) { }
        public static void LogError(object message) { }
    }
}
