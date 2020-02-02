using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.EditorTools.Runtime.IMGUI;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestTypesDropdownGUI")]
    public class TestTypesDropdownGUI : ScriptableObject
    {
        [SerializeField] private string m_typeName;
        [SerializeField, TypesDropdown(typeof(Attribute))] private string m_typeNameValue;
        [SerializeField, TypesDropdown(typeof(ScriptableObject))] private string m_typeNameValue2;
        [SerializeField, AssetGuid] private string m_assetGuid;
        [SerializeField, AssetGuid(typeof(Material))] private string m_assetGuid2;

        // [SerializeField, AssetGuid] private int m_invalidAssetGuidField;
        // [SerializeField, TypesDropdown] private int m_invalidTypeField;
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
            base.OnInspectorGUI();

            m_drawer.DrawGUILayout(new GUIContent("Test"));
        }

        private IEnumerable<Type> TypeCollector()
        {
            return TypeCache.GetTypesDerivedFrom<ScriptableObject>();
        }
    }
}
