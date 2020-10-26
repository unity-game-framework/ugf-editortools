using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Scopes
{
    [CreateAssetMenu(menuName = "Tests/TestSerializedObjectUpdateScopeAsset")]
    public class TestSerializedObjectUpdateScopeAsset : ScriptableObject
    {
        [SerializeField] private int m_value;

        public int Value { get { return m_value; } set { m_value = value; } }
    }

    [CustomEditor(typeof(TestSerializedObjectUpdateScopeAsset), true)]
    public class TestSerializedObjectUpdateScopeAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyValue;

        private void OnEnable()
        {
            m_propertyValue = serializedObject.FindProperty("m_value");
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyValue);
            }
        }
    }
}
