using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorElementsUtility
    {
        private static readonly DropdownSelection<DropdownItem<string>> m_selection = new DropdownSelection<DropdownItem<string>>();
        private static Styles m_styles;

        private class Styles
        {
            public GUIStyle TextFieldDropdownButtonStyle { get; } = new GUIStyle("TextFieldDropDown");
            public GUIStyle TextFieldDropdownFieldStyle { get; } = new GUIStyle("TextFieldDropDownText");
        }

        public static void TextFieldWithDropdown(SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            GUIContent label = EditorGUIUtility.TrTempContent(serializedProperty.displayName);

            TextFieldWithDropdown(label, serializedProperty, itemsHandler, options);
        }

        public static void TextFieldWithDropdown(GUIContent label, SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            TextFieldWithDropdown(position, label, serializedProperty, itemsHandler);
        }

        public static void TextFieldWithDropdown(Rect position, GUIContent label, SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler)
        {
            serializedProperty.stringValue = TextFieldWithDropdown(position, label, serializedProperty.stringValue, itemsHandler);
        }

        public static string TextFieldWithDropdown(string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            return TextFieldWithDropdown(GUIContent.none, value, itemsHandler, options);
        }

        public static string TextFieldWithDropdown(GUIContent label, string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TextFieldWithDropdown(position, label, value, itemsHandler);
        }

        public static string TextFieldWithDropdown(Rect position, GUIContent label, string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (itemsHandler == null) throw new ArgumentNullException(nameof(itemsHandler));

            Styles styles = GetStyles();

            var rectField = new Rect(position.x, position.y, position.width - position.height, position.height);
            var rectButton = new Rect(rectField.xMax, position.y, position.height, position.height);

            value = EditorGUI.TextField(rectField, label, value, styles.TextFieldDropdownFieldStyle);

            if (DropdownEditorGUIUtility.Dropdown(rectButton, GUIContent.none, GUIContent.none, m_selection, itemsHandler, out DropdownItem<string> selected, FocusType.Keyboard, styles.TextFieldDropdownButtonStyle))
            {
                value = selected.Value;
            }

            return value;
        }

        private static Styles GetStyles()
        {
            return m_styles ??= new Styles();
        }
    }
}
