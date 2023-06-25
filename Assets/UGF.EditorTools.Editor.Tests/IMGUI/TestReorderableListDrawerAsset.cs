using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestReorderableListDrawerAsset")]
    public class TestReorderableListDrawerAsset : ScriptableObject
    {
        [SerializeField] private List<Material> m_list = new List<Material>();

        public List<Material> List { get { return m_list; } }
    }

    [CustomEditor(typeof(TestReorderableListDrawerAsset))]
    public class TestReorderableListDrawerAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyList;
        private ReorderableListDrawer m_listDrawer;
        private CollectionDrawer m_collectionDrawer;

        private void OnEnable()
        {
            m_propertyList = serializedObject.FindProperty("m_list");

            m_listDrawer = new ReorderableListDrawer(m_propertyList)
            {
                EnableDragAndDropAdding = true
            };

            m_collectionDrawer = new CollectionDrawer(m_propertyList);

            m_listDrawer.Enable();
            m_collectionDrawer.Enable();
        }

        private void OnDisable()
        {
            m_listDrawer.Disable();
            m_collectionDrawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyList);

                m_listDrawer.DrawGUILayout();
                m_collectionDrawer.DrawGUILayout();
            }
        }
    }
}
