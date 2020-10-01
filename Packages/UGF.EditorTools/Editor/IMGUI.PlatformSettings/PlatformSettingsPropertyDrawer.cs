using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    [CustomPropertyDrawer(typeof(PlatformSettings<>), true)]
    internal class PlatformSettingsPropertyDrawer : PlatformSettingsPropertyDrawerBase
    {
        public PlatformSettingsPropertyDrawer()
        {
            Drawer.AddPlatformAllAvailable();
            Drawer.SettingsCreated += OnDrawerSettingsCreated;
        }

        protected override void OnDrawerSettingsCreated(string name, SerializedProperty propertySettings)
        {
            propertySettings.managedReferenceValue = null;
        }
    }
}
