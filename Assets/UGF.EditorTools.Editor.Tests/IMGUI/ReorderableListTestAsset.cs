using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Attributes;
using UGF.EditorTools.Runtime.Ids;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/ReorderableListTestAsset")]
    public class ReorderableListTestAsset : ScriptableObject
    {
        [List]
        [SerializeField] private List<ScriptableObject> m_list1;
        [List]
        [SerializeField] private List<ScriptableObject> m_list10;
        [SerializeField] private List<ScriptableObject> m_list11;
        [SerializeField] private List<Data1> m_list2;
        [SerializeField] private List<Data2> m_list3;
        [SerializeReference, ManagedReference] private List<IData> m_list4;
        [SerializeField] private List<Entry> m_list5;
        [AssetId]
        [SerializeField] private List<GlobalId> m_list6;

        [Serializable]
        public class Entry
        {
            [SerializeField] private string m_key;
            [SerializeField] private string m_value;
        }

        public interface IData
        {
        }

        [Serializable]
        public class Data1 : IData
        {
            [SerializeField] private bool m_bool;
            [SerializeField] private int m_int;
            [SerializeField] private float m_float;
            [SerializeField] private Quaternion m_quaternion;
        }

        [Serializable]
        public class Data2 : IData
        {
            [SerializeField] private bool m_bool2;
            [SerializeField] private Object m_object;
        }
    }

    // [CustomEditor(typeof(ReorderableListTestAsset), true)]
    public class ReorderableListTestAssetEditor : UnityEditor.Editor
    {
        private EditorListDrawer m_drawer1;
        private ReorderableListDrawer m_drawer2;
        private ReorderableListDrawer m_drawer3;
        private ReorderableListSelectionDrawerByPath m_drawer3Selection;
        private ReorderableListDrawer m_drawer4;
        private ReorderableListKeyAndValueDrawer m_drawer5;
        private ReorderableListDrawer m_drawer6;

        private void OnEnable()
        {
            m_drawer1 = new EditorListDrawer(serializedObject.FindProperty("m_list1"));
            m_drawer2 = new ReorderableListDrawer(serializedObject.FindProperty("m_list2"));
            m_drawer3 = new ReorderableListDrawer(serializedObject.FindProperty("m_list3"));
            m_drawer3.List.draggable = false;
            m_drawer3Selection = new ReorderableListSelectionDrawerByPath(m_drawer3, "m_object");
            m_drawer4 = new ReorderableListDrawer(serializedObject.FindProperty("m_list4"));

            m_drawer5 = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_list5"))
            {
                List =
                {
                    draggable = false
                }
            };

            m_drawer6 = new ReorderableListDrawer(serializedObject.FindProperty("m_list6"))
            {
                DisplayAsSingleLine = true
            };

            m_drawer1.Enable();
            m_drawer3Selection.Enable();
            m_drawer5.Enable();
            m_drawer6.Enable();
        }

        private void OnDisable()
        {
            m_drawer1.Disable();
            m_drawer3Selection.Disable();
            m_drawer5.Disable();
            m_drawer6.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            m_drawer1.DrawGUILayout();

            using (new EditorGUI.IndentLevelScope(2))
            {
                m_drawer2.DrawGUILayout();
            }

            using (new EditorGUI.IndentLevelScope(5))
            {
                m_drawer3.DrawGUILayout();
                m_drawer5.DrawGUILayout();
            }

            m_drawer3Selection.DrawGUILayout();

            m_drawer4.DrawGUILayout();
            m_drawer5.DrawGUILayout();
            m_drawer6.DrawGUILayout();

            serializedObject.ApplyModifiedProperties();

            m_drawer1.DrawSelectedLayout();
        }
    }
}
