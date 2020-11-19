using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public abstract class TypesDropdownAttributePropertyDrawer<TAttribute> : PropertyDrawerTyped<TAttribute> where TAttribute : PropertyAttribute
    {
        protected DropdownDrawer<DropdownItem<Type>> Drawer { get { return m_drawer ??= OnCreateDrawer(); } }

        protected DropdownItem<Type> NoneItem { get; } = new DropdownItem<Type>("None")
        {
            Priority = int.MaxValue
        };

        private DropdownDrawer<DropdownItem<Type>> m_drawer;

        protected TypesDropdownAttributePropertyDrawer(SerializedPropertyType propertyType) : base(propertyType)
        {
        }

        protected abstract DropdownDrawer<DropdownItem<Type>> OnCreateDrawer();

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            Drawer.DrawGUI(position, label, property);
        }

        protected virtual void OnGetItems(ICollection<DropdownItem<Type>> items)
        {
            items.Add(NoneItem);
        }

        protected IEnumerable<DropdownItem<Type>> GetItems()
        {
            var items = new List<DropdownItem<Type>>();

            OnGetItems(items);

            return items;
        }
    }
}
