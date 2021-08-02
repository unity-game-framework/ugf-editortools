using UGF.EditorTools.Editor.Preferences;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Preferences
{
    [CreateAssetMenu(menuName = "Tests/TestPreferenceEditorValueAsset")]
    public class TestPreferenceEditorValueAsset : ScriptableObject
    {
    }

    [CustomEditor(typeof(TestPreferenceEditorValueAsset))]
    public class TestPreferenceEditorValueAssetEditor : UnityEditor.Editor
    {
        private readonly PreferenceEditorValue<bool> m_bool = new PreferenceEditorValue<bool>("TestPreferenceEditorValue.Bool");
        private readonly PreferenceEditorValue<float> m_float = new PreferenceEditorValue<float>("TestPreferenceEditorValue.Float");
        private readonly PreferenceEditorValue<Vector3> m_vector3 = new PreferenceEditorValue<Vector3>("TestPreferenceEditorValue.Vector3");

        private void OnEnable()
        {
            m_bool.Enable();
            m_float.Enable();
            m_vector3.Enable();
        }

        private void OnDisable()
        {
            m_bool.Disable();
            m_float.Disable();
            m_vector3.Disable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_bool.Value = EditorGUILayout.Toggle("Bool", m_bool.Value);
            m_float.Value = EditorGUILayout.FloatField("Float", m_float.Value);
            m_vector3.Value = EditorGUILayout.Vector3Field("Vector3", m_vector3.Value);
        }
    }
}
