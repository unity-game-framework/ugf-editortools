using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    [CustomPropertyDrawer(typeof(ManagedReferenceAttribute), true)]
    internal class ManagedReferenceAttributeDrawer : PropertyDrawerTyped<ManagedReferenceAttribute>
    {
        private readonly ManagedReferenceDropdownDrawer m_drawer;

        private readonly DropdownItem<Type> m_noneItem = new DropdownItem<Type>("None")
        {
            Priority = int.MaxValue
        };

        public ManagedReferenceAttributeDrawer() : base(SerializedPropertyType.ManagedReference)
        {
            m_drawer = new ManagedReferenceDropdownDrawer(OnGetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            m_drawer.DrawGUI(position, label, property);

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        private IEnumerable<DropdownItem<Type>> OnGetItems()
        {
            Type targetType = GetTargetType();
            List<DropdownItem<Type>> items = ManagedReferenceEditorUtility.GetTypeItems(targetType, Attribute.DisplayFullPath);

            items.Add(m_noneItem);

            return items;
        }

        private Type GetTargetType()
        {
            Type type;

            if (Attribute.HasTargetType)
            {
                type = Attribute.TargetType;
            }
            else
            {
                type = fieldInfo.FieldType;

                if (type.IsArray)
                {
                    type = type.GetElementType();
                }
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            return type;
        }
    }
}
