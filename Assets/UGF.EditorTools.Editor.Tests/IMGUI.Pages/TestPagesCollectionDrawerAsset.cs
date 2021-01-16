using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Pages;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Pages
{
    [CreateAssetMenu(menuName = "Tests/TestPagesCollectionDrawerAsset")]
    public class TestPagesCollectionDrawerAsset : ScriptableObject
    {
        [SerializeField] private List<string> m_list = new List<string>();
        [SerializeField] private List<Vector4> m_list2 = new List<Vector4>();

        public List<string> List { get { return m_list; } }
        public List<Vector4> List2 { get { return m_list2; } }
    }

    [CustomEditor(typeof(TestPagesCollectionDrawerAsset), true)]
    public class TestPagesCollectionDrawerAssetEditor : UnityEditor.Editor
    {
        private PagesCollectionDrawer m_drawer;
        private PagesCollectionDrawer m_drawer2;

        private void OnEnable()
        {
            m_drawer = new PagesCollectionDrawer(serializedObject.FindProperty("m_list"));
            m_drawer2 = new PagesCollectionDrawer(serializedObject.FindProperty("m_list2"));
            m_drawer.Enable();
            m_drawer2.Enable();
        }

        private void OnDisable()
        {
            m_drawer.Disable();
            m_drawer2.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_drawer.DrawGUILayout();

                using (new IndentIncrementScope(5))
                {
                    m_drawer2.DrawGUILayout();
                }
            }
        }
    }
}
