using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public static class PlatformSettingsExtensions
    {
        public static bool HasSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup) where T : class
        {
            string name = buildTargetGroup.ToString();

            return platformSettings.HasSettings(name);
        }

        public static bool TrySetSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup, T settings) where T : class
        {
            string name = buildTargetGroup.ToString();

            return platformSettings.TrySetSettings(name, settings);
        }

        public static bool TryGetSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup, out T settings) where T : class
        {
            string name = buildTargetGroup.ToString();

            return platformSettings.TryGetSettings(name, out settings);
        }

        public static bool TryClearSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup) where T : class
        {
            string name = buildTargetGroup.ToString();

            return platformSettings.TryClearSettings(name);
        }
    }
}
