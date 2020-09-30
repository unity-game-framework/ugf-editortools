using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    [CustomPropertyDrawer(typeof(PlatformSettings<>), true)]
    internal class PlatformSettingsPropertyDrawer : PlatformSettingsPropertyDrawerBase
    {
        public PlatformSettingsPropertyDrawer()
        {
            var platforms = new List<BuildTargetGroup>();

            PlatformSettingsEditorUtility.GetPlatformsAvailable(platforms);

            for (int i = 0; i < platforms.Count; i++)
            {
                BuildTargetGroup platform = platforms[i];

                Drawer.AddPlatform(platform);
            }

            Drawer.SettingsCreated += OnDrawerSettingsCreated;
        }

        protected override void OnDrawerSettingsCreated(string name, SerializedProperty propertySettings)
        {
            propertySettings.managedReferenceValue = null;
        }
    }
}
