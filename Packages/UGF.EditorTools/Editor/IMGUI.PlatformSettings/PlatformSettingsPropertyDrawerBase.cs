using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public abstract class PlatformSettingsPropertyDrawerBase : PropertyDrawerBase
    {
        protected PlatformSettingsDrawer Drawer { get; set; } = new PlatformSettingsDrawer();

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");

            Drawer.DrawGUI(position, propertyGroups);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");

            return Drawer.GetHeight(propertyGroups);
        }
    }
}
