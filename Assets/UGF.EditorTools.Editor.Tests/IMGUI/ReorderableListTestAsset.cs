using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/ReorderableListTestAsset")]
    public class ReorderableListTestAsset : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> m_list1;
        [SerializeField] private List<Data1> m_list2;
        [SerializeField] private List<Data2> m_list3;
        [SerializeReference, ManagedReference] private List<IData> m_list4;

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

    [CustomEditor(typeof(ReorderableListTestAsset), true)]
    public class ReorderableListTestAssetEditor : UnityEditor.Editor
    {
        private EditorListDrawer m_drawer1;
        private ReorderableListDrawer m_drawer2;
        private ReorderableListDrawer m_drawer3;
        private ReorderableListDrawer m_drawer4;

        private void OnEnable()
        {
            m_drawer1 = new EditorListDrawer(serializedObject.FindProperty("m_list1"));
            m_drawer2 = new ReorderableListDrawer(serializedObject.FindProperty("m_list2"));
            m_drawer3 = new ReorderableListDrawer(serializedObject.FindProperty("m_list3"));
            m_drawer3.List.draggable = false;
            m_drawer4 = new ReorderableListDrawer(serializedObject.FindProperty("m_list4"));
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
            }

            m_drawer4.DrawGUILayout();

            serializedObject.ApplyModifiedProperties();

            m_drawer1.DrawSelectedLayout();
        }
    }
}
