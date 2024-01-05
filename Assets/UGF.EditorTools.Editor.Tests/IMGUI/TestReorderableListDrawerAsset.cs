using System;
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
        [SerializeField] private List<Entry> m_entries = new List<Entry>();

        public List<Material> List { get { return m_list; } }
        public List<Entry> Entries { get { return m_entries; } }

        [Serializable]
        public struct Entry
        {
            [SerializeField] private int m_value;
            [SerializeField] private string m_text;

            public int Value { get { return m_value; } set { m_value = value; } }
            public string Text { get { return m_text; } set { m_text = value; } }
        }
    }

    [CustomEditor(typeof(TestReorderableListDrawerAsset))]
    public class TestReorderableListDrawerAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyList;
        private ReorderableListDrawer m_listDrawer;
        private CollectionDrawer m_collectionDrawer;
        private ReorderableListDrawer m_listEntries;

        private void OnEnable()
        {
            m_propertyList = serializedObject.FindProperty("m_list");

            m_listDrawer = new ReorderableListDrawer(m_propertyList)
            {
                EnableDragAndDropAdding = true
            };

            m_collectionDrawer = new CollectionDrawer(m_propertyList);

            m_listEntries = new ReorderableListDrawer(serializedObject.FindProperty("m_entries"))
            {
                DisplayElementFoldout = false,
                List =
                {
                    draggable = true
                }
            };

            m_listDrawer.Enable();
            m_collectionDrawer.Enable();
            m_listEntries.Disable();
        }

        private void OnDisable()
        {
            m_listDrawer.Disable();
            m_collectionDrawer.Disable();
            m_listEntries.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyList);

                m_listDrawer.DrawGUILayout();
                m_collectionDrawer.DrawGUILayout();
                m_listEntries.DrawGUILayout();
            }
        }
    }
}
