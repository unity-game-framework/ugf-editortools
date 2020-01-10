using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    [CustomPropertyDrawer(typeof(TypesDropdownAttribute), true)]
    internal class TypesDropdownAttributeDrawer : PropertyDrawer
    {
        private readonly GUIContent m_contentNone = new GUIContent("None");
        private readonly GUIContent m_contentMissing = new GUIContent("Missing");
        private TypesDropdown m_dropdown;
        private Type m_type;
        private bool m_assign;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_dropdown == null)
            {
                Initialize();
            }

            EditorGUI.BeginProperty(position, label, property);

            Rect rect = EditorGUI.PrefixLabel(position, label);
            GUIContent content = m_contentNone;

            if (!string.IsNullOrEmpty(property.stringValue))
            {
                var type = Type.GetType(property.stringValue);

                content = type != null ? new GUIContent(type.Name) : m_contentMissing;
            }

            if (EditorGUI.DropdownButton(rect, content, FocusType.Keyboard) && !m_assign)
            {
                m_dropdown.Show(rect);
            }

            if (m_assign)
            {
                property.stringValue = m_type?.AssemblyQualifiedName ?? string.Empty;

                m_assign = false;
            }

            EditorGUI.EndProperty();
        }

        private void Initialize()
        {
            m_dropdown = new TypesDropdown(TypesCollector);
            m_dropdown.Selected += OnDropdownSelected;
        }

        private IEnumerable<Type> TypesCollector()
        {
            var typesDropdownAttribute = (TypesDropdownAttribute)attribute;

            return TypeCache.GetTypesDerivedFrom(typesDropdownAttribute.TargetType);
        }

        private void OnDropdownSelected(Type type)
        {
            m_type = type;
            m_assign = true;
        }
    }
}
