using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownGUIUtility
    {
        public static bool DropdownButton(string content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return DropdownButton(GUIContent.none, new GUIContent(content), out dropdownPosition, focusType, options);
        }

        public static bool DropdownButton(string label, string content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            return DropdownButton(new GUIContent(label), new GUIContent(content), out dropdownPosition, focusType);
        }

        public static bool DropdownButton(GUIContent label, GUIContent content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DropdownButton(position, label, content, out dropdownPosition, focusType);
        }

        public static bool DropdownButton(Rect position, string content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return DropdownButton(position, GUIContent.none, new GUIContent(content), out dropdownPosition, focusType);
        }

        public static bool DropdownButton(Rect position, string label, string content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            return DropdownButton(position, new GUIContent(label), new GUIContent(content), out dropdownPosition, focusType);
        }

        public static bool DropdownButton(Rect position, GUIContent label, GUIContent content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (label != GUIContent.none)
            {
                position = EditorGUI.PrefixLabel(position, label);
            }

            dropdownPosition = position;

            return EditorGUI.DropdownButton(position, content, focusType);
        }
    }
}
