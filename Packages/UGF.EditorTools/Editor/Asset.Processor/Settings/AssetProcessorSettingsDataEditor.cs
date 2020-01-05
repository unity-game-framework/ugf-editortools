using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Processor.Settings
{
    [CustomEditor(typeof(AssetProcessorSettingsData))]
    internal class AssetProcessorSettingsDataEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyActive;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyActive = serializedObject.FindProperty("m_active");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            EditorGUILayout.PropertyField(m_propertyActive);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
