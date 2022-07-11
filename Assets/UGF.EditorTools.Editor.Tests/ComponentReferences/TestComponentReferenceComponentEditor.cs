using UGF.EditorTools.Editor.ComponentReferences;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.Tests.ComponentReferences;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.ComponentReferences
{
    [CustomEditor(typeof(TestComponentReferenceComponent), true)]
    public class TestComponentReferenceComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyReference;
        private SerializedProperty m_propertyReference1;
        private SerializedProperty m_propertyReference2;
        private SerializedProperty m_propertyList1;
        private ComponentReferenceListDrawer m_list2;
        private ComponentIdReferenceListDrawer m_list3;

        private void OnEnable()
        {
            m_propertyReference = serializedObject.FindProperty("m_reference");
            m_propertyReference1 = serializedObject.FindProperty("m_reference1");
            m_propertyReference2 = serializedObject.FindProperty("m_reference2");
            m_propertyList1 = serializedObject.FindProperty("m_list");
            m_list2 = new ComponentReferenceListDrawer(serializedObject.FindProperty("m_list2"));
            m_list3 = new ComponentIdReferenceListDrawer(serializedObject.FindProperty("m_list3"));
            m_list2.Enable();
            m_list3.Enable();
        }

        private void OnDisable()
        {
            m_list2.Disable();
            m_list3.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyReference);
                EditorGUILayout.PropertyField(m_propertyReference1);
                EditorGUILayout.PropertyField(m_propertyReference2);
                EditorGUILayout.PropertyField(m_propertyList1);

                m_list2.DrawGUILayout();
                m_list3.DrawGUILayout();
            }
        }
    }
}
