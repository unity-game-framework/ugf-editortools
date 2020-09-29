using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.SettingsGroups
{
    [CustomEditor(typeof(TestSettingsGroupsAsset), true)]
    public class TestSettingsGroupsAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyGroups;
        private SerializedProperty m_propertyFirst;
        private SettingsGroupsDrawer m_drawer = new SettingsGroupsDrawer();

        private void OnEnable()
        {
            SerializedProperty propertySettings = serializedObject.FindProperty("m_settings");

            m_propertyGroups = propertySettings.FindPropertyRelative("m_groups");
            m_propertyFirst = m_propertyGroups.GetArrayElementAtIndex(0).FindPropertyRelative("m_settings");

            m_drawer.Toolbar.TabLabels.AddRange(new[] { new GUIContent("First"), new GUIContent("Second") });
            m_drawer.Toolbar.Count = m_drawer.Toolbar.TabLabels.Count;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginBuildTargetSelectionGrouping();

            using (new IndentIncrementScope(1))
            {
                EditorGUILayout.PropertyField(m_propertyFirst);
            }

            EditorGUILayout.EndBuildTargetSelectionGrouping();

            m_drawer.DrawGUILayout(m_propertyGroups);
        }
    }
}
