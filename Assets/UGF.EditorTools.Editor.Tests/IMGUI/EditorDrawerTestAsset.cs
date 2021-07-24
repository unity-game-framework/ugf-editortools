using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/EditorDrawerTestAsset")]
    public class EditorDrawerTestAsset : ScriptableObject
    {
        [SerializeField] private ScriptableObject m_target;
        [SerializeField] private bool m_displayTitlebar;

        public ScriptableObject Target { get { return m_target; } set { m_target = value; } }
        public bool DisplayTitlebar { get { return m_displayTitlebar; } set { m_displayTitlebar = value; } }
    }

    [CustomEditor(typeof(EditorDrawerTestAsset), true)]
    public class EditorDrawerTestAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyTarget;
        private SerializedProperty m_propertyDisplayTitlebar;
        private EditorObjectReferenceDrawer m_drawer;

        private void OnEnable()
        {
            m_propertyTarget = serializedObject.FindProperty("m_target");
            m_propertyDisplayTitlebar = serializedObject.FindProperty("m_displayTitlebar");
            m_drawer = new EditorObjectReferenceDrawer(m_propertyTarget);
        }

        public override void OnInspectorGUI()
        {
            EditorIMGUIUtility.DrawScriptProperty(serializedObject);

            EditorGUILayout.PropertyField(m_propertyTarget);
            EditorGUILayout.PropertyField(m_propertyDisplayTitlebar);

            m_drawer.Drawer.DisplayTitlebar = m_propertyDisplayTitlebar.boolValue;
            m_drawer.DrawGUILayout();
        }
    }
}
