using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    [CustomPropertyDrawer(typeof(TypeReferenceDropdownAttribute), true)]
    internal class TypeReferenceDropdownAttributePropertyDrawer : TypesDropdownAttributePropertyDrawer<TypeReferenceDropdownAttribute>
    {
        public TypeReferenceDropdownAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override DropdownDrawer<DropdownItem<Type>> OnCreateDrawer()
        {
            return new TypesDropdownDrawer(GetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty propertyValue = property.FindPropertyRelative("m_value");

            if (propertyValue != null)
            {
                base.OnDrawProperty(position, propertyValue, label);
            }
            else
            {
                OnDrawPropertyDefault(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override void OnGetItems(ICollection<DropdownItem<Type>> items)
        {
            Type targetType = GetTargetType();

            items.Add(NoneItem);

            TypesDropdownEditorUtility.GetTypeItems(items, targetType, Attribute.DisplayFullPath, Attribute.DisplayAssemblyName);
        }

        private Type GetTargetType()
        {
            if (Attribute.HasTargetType)
            {
                return Attribute.TargetType;
            }

            Type fieldType = fieldInfo.FieldType;
            Type argumentType = fieldType.GetGenericArguments()[0];

            return argumentType;
        }
    }
}
