using UGF.EditorTools.Editor.Platforms;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public static class PlatformSettingsExtensions
    {
        public static bool HasSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup) where T : class
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(buildTargetGroup);

            return platformSettings.HasSettings(platform.Name);
        }

        public static bool TrySetSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup, T settings) where T : class
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(buildTargetGroup);

            return platformSettings.TrySetSettings(platform.Name, settings);
        }

        public static bool TryGetSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup, out T settings) where T : class
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(buildTargetGroup);

            return platformSettings.TryGetSettings(platform.Name, out settings);
        }

        public static bool TryClearSettings<T>(this PlatformSettings<T> platformSettings, BuildTargetGroup buildTargetGroup) where T : class
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(buildTargetGroup);

            return platformSettings.TryClearSettings(platform.Name);
        }
    }
}
