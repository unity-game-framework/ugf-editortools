using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/EditorDrawerTestAsset")]
    public class EditorDrawerTestAsset : ScriptableObject
    {
        [SerializeField] private ScriptableObject m_target;
        [AssetId(typeof(ScriptableObject))]
        [SerializeField] private GlobalId m_target2;
        [SerializeField] private bool m_displayTitlebar;

        public ScriptableObject Target { get { return m_target; } set { m_target = value; } }
        public GlobalId Target2 { get { return m_target2; } set { m_target2 = value; } }
        public bool DisplayTitlebar { get { return m_displayTitlebar; } set { m_displayTitlebar = value; } }
    }

    [CustomEditor(typeof(EditorDrawerTestAsset), true)]
    public class EditorDrawerTestAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyTarget;
        private SerializedProperty m_propertyTarget2;
        private SerializedProperty m_propertyDisplayTitlebar;
        private EditorObjectReferenceDrawer m_drawer;
        private EditorObjectReferenceIdDrawer m_drawer2;
        private DropAreaDrawer m_dropAreaDrawer;

        private void OnEnable()
        {
            m_propertyTarget = serializedObject.FindProperty("m_target");
            m_propertyTarget2 = serializedObject.FindProperty("m_target2");
            m_propertyDisplayTitlebar = serializedObject.FindProperty("m_displayTitlebar");
            m_drawer = new EditorObjectReferenceDrawer(m_propertyTarget);
            m_drawer2 = new EditorObjectReferenceIdDrawer(m_propertyTarget2);
            m_dropAreaDrawer = new DropAreaDrawer(typeof(ScriptableObject));
            m_dropAreaDrawer.Handler.Accepted += OnDropAreaDrawerAccepted;
            m_dropAreaDrawer.Enable();
        }

        private void OnDisable()
        {
            m_dropAreaDrawer.Handler.Accepted -= OnDropAreaDrawerAccepted;
            m_dropAreaDrawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            EditorIMGUIUtility.DrawScriptProperty(serializedObject);

            EditorGUILayout.PropertyField(m_propertyTarget);
            EditorGUILayout.PropertyField(m_propertyTarget2);
            EditorGUILayout.PropertyField(m_propertyDisplayTitlebar);

            m_drawer.Drawer.DisplayTitlebar = m_propertyDisplayTitlebar.boolValue;
            m_drawer.DrawGUILayout();
            m_drawer2.DrawGUILayout();
            m_dropAreaDrawer.DrawGUILayout();
        }

        private void OnDropAreaDrawerAccepted(Object asset)
        {
            Debug.Log($"Drop area accepted: '{asset}'.");
        }
    }
}
