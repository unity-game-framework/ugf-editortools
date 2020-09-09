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
        private TypesDropdownDrawer m_drawer;

        public TypesDropdownAttributeDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_drawer == null)
            {
                List<DropdownItem<Type>> items = TypesDropdownEditorUtility.GetTypeItems(Attribute.TargetType);

                m_drawer = new TypesDropdownDrawer(items);
            }

            m_drawer.DrawGUI(position, label, property);
        }
    }
}
