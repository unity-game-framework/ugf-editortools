using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestTypesDropdownGUI")]
    public class TestTypesDropdownGUI : ScriptableObject, ITest
    {
        [SerializeField, Disable] private string m_typeName;

        [SerializeField, TypesDropdown(typeof(Attribute), DisplayFullPath = false)]
        private string m_typeNameValue;

        [SerializeField, TypesDropdown(typeof(ScriptableObject))]
        private string m_typeNameValue2;

        [SerializeField, AssetGuid] private string m_assetGuid;

        [SerializeField, AssetGuid(typeof(Material))]
        private string m_assetGuid2;

        [SerializeField, AssetGuid(typeof(Scene))]
        private string m_assetScene;

        [SerializeField, AssetType] private Object m_assetType1;

        [SerializeField, AssetType(typeof(ITest))]
        private Object m_assetType2;

        [SerializeField] private Indent1 m_indent1;

        [SerializeField, AssetGuid] private int m_invalidAssetGuidField;
        [SerializeField, TypesDropdown] private int m_invalidTypeField;

        [Serializable]
        public class Indent1
        {
            [SerializeField] private Indent2 m_indent2;
        }

        [Serializable]
        public class Indent2
        {
            [SerializeField, AssetGuid] private string m_assetGuid;
            [SerializeField, TypesDropdown] private string m_type;
        }
    }

    public interface ITest
    {
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TestTypesDropdownGUI), true)]
    public class TestTypesDropdownGUIEditor : UnityEditor.Editor
    {
        private TypesDropdownDrawer m_drawer;
        private SerializedProperty m_propertyTypeName;

        private void OnEnable()
        {
            m_drawer = new TypesDropdownDrawer(() =>
            {
                var items = new List<DropdownItem<Type>>();

                TypesDropdownEditorUtility.GetTypeItems(items, typeof(ScriptableObject), false, false);

                return items;
            });

            m_propertyTypeName = serializedObject.FindProperty("m_typeName");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_drawer.DrawGUILayout(new GUIContent("Test"), m_propertyTypeName);
        }
    }
}
