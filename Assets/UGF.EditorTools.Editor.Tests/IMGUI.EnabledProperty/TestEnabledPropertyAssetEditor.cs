using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.IMGUI.EnabledProperty
{
    [CustomEditor(typeof(TestEnabledPropertyAsset), true)]
    public class TestEnabledPropertyAssetEditor : UnityEditor.Editor
    {
        private EnabledPropertyListDrawer m_list1;
        private EnabledPropertyListDrawer m_list2;

        private void OnEnable()
        {
            m_list1 = new EnabledPropertyListDrawer(serializedObject.FindProperty("m_list1"));
            m_list2 = new EnabledPropertyListDrawer(serializedObject.FindProperty("m_list2"));

            m_list1.Enable();
            m_list2.Enable();
        }

        private void OnDisable()
        {
            m_list1.Disable();
            m_list2.Disable();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            using (new IndentIncrementScope(1))
            {
                DrawDefaultInspector();
            }

            using (new IndentIncrementScope(2))
            {
                DrawDefaultInspector();
            }

            EditorGUILayout.LabelField("Lists", EditorStyles.boldLabel);

            serializedObject.UpdateIfRequiredOrScript();

            using (new IndentIncrementScope(4))
            {
                m_list1.DrawGUILayout();
                m_list2.DrawGUILayout();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
