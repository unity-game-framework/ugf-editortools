using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestTypesDropdownGUI")]
    public class TestTypesDropdownGUI : ScriptableObject
    {
        [SerializeField] private string m_typeName;
    }

    [CustomEditor(typeof(TestTypesDropdownGUI), true)]
    public class TestTypesDropdownGUIEditor : UnityEditor.Editor
    {
        private TypesDropdownDrawer m_drawer;

        private void OnEnable()
        {
            SerializedProperty propertyTypeName = serializedObject.FindProperty("m_typeName");

            m_drawer = new TypesDropdownDrawer(propertyTypeName, TypeCollector);
        }

        public override void OnInspectorGUI()
        {
            m_drawer.DrawGUILayout();
        }

        private IEnumerable<Type> TypeCollector()
        {
            return TypeCache.GetTypesDerivedFrom<ScriptableObject>();
        }
    }
}
