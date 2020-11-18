using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    [CustomPropertyDrawer(typeof(TypesDropdownAttribute), true)]
    internal class TypesDropdownAttributeDrawer : PropertyDrawerTyped<TypesDropdownAttribute>
    {
        private readonly TypesDropdownDrawer m_drawer;
        private readonly DropdownItem<Type> m_noneItem = new DropdownItem<Type>("None")
        {
            Priority = int.MaxValue
        };

        public TypesDropdownAttributeDrawer() : base(SerializedPropertyType.String)
        {
            m_drawer = new TypesDropdownDrawer(OnGetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            m_drawer.DrawGUI(position, label, property);
        }

        private IEnumerable<DropdownItem<Type>> OnGetItems()
        {
            var items = new List<DropdownItem<Type>>();

            TypesDropdownEditorUtility.GetTypeItems(items, Attribute.TargetType, Attribute.DisplayFullPath, Attribute.DisplayAssemblyName);

            items.Add(m_noneItem);

            return items;
        }
    }
}
