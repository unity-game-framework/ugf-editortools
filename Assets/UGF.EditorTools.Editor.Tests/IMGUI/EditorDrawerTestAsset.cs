using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/EditorDrawerTestAsset")]
    public class EditorDrawerTestAsset : ScriptableObject
    {
        [SerializeField] private ScriptableObject m_target;
    }

    [CustomEditor(typeof(EditorDrawerTestAsset), true)]
    public class EditorDrawerTestAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_property;
        private EditorObjectReferenceDrawer m_drawer;

        private void OnEnable()
        {
            m_property = serializedObject.FindProperty("m_target");
            m_drawer = new EditorObjectReferenceDrawer(m_property);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_property);

            m_drawer.DrawGUILayout();
        }
    }
}
