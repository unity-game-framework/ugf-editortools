using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdownDrawer
    {
        public SerializedProperty SerializedProperty { get; }

        public event Action<Type> Selected { add { m_dropdown.Selected += value; } remove { m_dropdown.Selected -= value; } }

        private readonly TypesDropdown m_dropdown;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ContentNone { get; } = new GUIContent("None");
        }

        public TypesDropdownDrawer(SerializedProperty serializedProperty, Func<IEnumerable<Type>> typeCollector)
        {
            if (typeCollector == null) throw new ArgumentNullException(nameof(typeCollector));

            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

            m_dropdown = new TypesDropdown(typeCollector);
            m_dropdown.Selected += OnDropdownSelected;
        }

        public void DrawGUILayout()
        {
            DrawGUILayout(GUIContent.none);
        }

        public void DrawGUILayout(GUIContent label)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none);

            DrawGUI(position, label);
        }

        public void DrawGUI(Rect position)
        {
            DrawGUI(position, GUIContent.none);
        }

        public void DrawGUI(Rect position, GUIContent label)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            if (m_styles == null)
            {
                m_styles = new Styles();
            }

            if (label != GUIContent.none)
            {
                position = EditorGUI.PrefixLabel(position, label);
            }

            var type = Type.GetType(SerializedProperty.stringValue);
            GUIContent typeButtonContent = type != null ? new GUIContent(type.Name) : m_styles.ContentNone;

            if (EditorGUI.DropdownButton(position, typeButtonContent, FocusType.Keyboard))
            {
                m_dropdown.Show(position);
            }
        }

        private void OnDropdownSelected(Type type)
        {
            SerializedProperty.stringValue = type?.AssemblyQualifiedName ?? string.Empty;
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
