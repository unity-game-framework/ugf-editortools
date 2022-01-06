using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestEditorIMGUIElementsUtilityAsset")]
    public class TestEditorIMGUIElementsUtilityAsset : ScriptableObject
    {
        [SerializeField] private string m_first = "First";
        [SerializeField] private string m_second = "Second";

        public string First { get { return m_first; } set { m_first = value; } }
        public string Second { get { return m_second; } set { m_second = value; } }
    }

    [CustomEditor(typeof(TestEditorIMGUIElementsUtilityAsset), true)]
    public class TestEditorIMGUIElementsUtilityAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyFirst;
        private SerializedProperty m_propertySecond;

        private void OnEnable()
        {
            m_propertyFirst = serializedObject.FindProperty("m_first");
            m_propertySecond = serializedObject.FindProperty("m_second");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_propertyFirst);
            EditorGUILayout.PropertyField(m_propertySecond);

            for (int i = 0; i < 3; i++)
            {
                using (new IndentLevelScope(i))
                {
                    EditorIMGUIElementsUtility.DrawPairedField(m_propertyFirst, m_propertySecond);
                }
            }
        }
    }
}
