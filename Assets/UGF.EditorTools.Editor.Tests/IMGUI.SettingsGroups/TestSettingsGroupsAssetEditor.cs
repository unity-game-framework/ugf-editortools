using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.SettingsGroups
{
    [CustomEditor(typeof(TestSettingsGroupsAsset), true)]
    public class TestSettingsGroupsAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyGroups;
        private SettingsGroupsDrawer m_drawer = new SettingsGroupsDrawer();

        private void OnEnable()
        {
            SerializedProperty propertySettings = serializedObject.FindProperty("m_settings");

            m_propertyGroups = propertySettings.FindPropertyRelative("m_groups");

            m_drawer.AddGroup("First", new GUIContent("First"));
            m_drawer.AddGroup("Second", new GUIContent("Second"));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            m_drawer.DrawGUILayout(m_propertyGroups);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
