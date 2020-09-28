using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.IMGUI.SettingsGroups
{
    [CustomEditor(typeof(TestSettingsGroupsAsset), true)]
    public class TestSettingsGroupsAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyGroups;
        private SerializedProperty m_propertyFirst;
        private SettingsGroupsDrawer m_drawer = new SettingsGroupsDrawer();
        private int m_selected;

        private void OnEnable()
        {
            SerializedProperty propertySettings = serializedObject.FindProperty("m_settings");

            m_propertyGroups = propertySettings.FindPropertyRelative("m_groups");
            m_propertyFirst = m_propertyGroups.GetArrayElementAtIndex(0).FindPropertyRelative("m_settings");
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

            m_selected = m_drawer.DrawGUILayout(m_propertyGroups, m_selected);
        }
    }
}
