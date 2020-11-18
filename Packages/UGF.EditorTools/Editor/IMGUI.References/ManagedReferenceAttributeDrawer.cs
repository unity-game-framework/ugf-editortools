using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    [CustomPropertyDrawer(typeof(ManagedReferenceAttribute), true)]
    internal class ManagedReferenceAttributeDrawer : TypesDropdownAttributePropertyDrawer<ManagedReferenceAttribute>
    {
        public ManagedReferenceAttributeDrawer() : base(SerializedPropertyType.ManagedReference)
        {
        }

        protected override DropdownDrawer<DropdownItem<Type>> OnCreateDrawer()
        {
            return new ManagedReferenceDropdownDrawer(GetItems);
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnDrawProperty(position, property, label);

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        protected override void OnGetItems(ICollection<DropdownItem<Type>> items)
        {
            base.OnGetItems(items);

            TypesDropdownEditorUtility.GetTypeItems(items, OnValidate, Attribute.DisplayFullPath, false);
        }

        private bool OnValidate(Type type)
        {
            Type targetType = GetTargetType();

            return targetType.IsAssignableFrom(type) && ManagedReferenceEditorUtility.IsValidType(type);
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
