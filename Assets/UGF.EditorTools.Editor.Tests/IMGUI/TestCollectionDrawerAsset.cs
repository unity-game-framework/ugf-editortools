using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestCollectionDrawerAsset")]
    public class TestCollectionDrawerAsset : ScriptableObject
    {
        [SerializeField] private List<string> m_list = new List<string>();

        public List<string> List { get { return m_list; } }
    }

    [CustomEditor(typeof(TestCollectionDrawerAsset))]
    public class TestCollectionDrawerAssetEditor : UnityEditor.Editor
    {
        private CollectionDrawer m_drawer;

        private void OnEnable()
        {
            m_drawer = new CollectionDrawer(serializedObject.FindProperty("m_list"));
            m_drawer.Enable();
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_drawer.DrawGUILayout();
            }
        }
    }
}
