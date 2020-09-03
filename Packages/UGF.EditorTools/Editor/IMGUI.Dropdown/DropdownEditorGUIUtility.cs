using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorGUIUtility
    {
        public static T Dropdown<T>(GUIContent label, GUIContent content, IEnumerable<T> items, T item, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options) where T : DropdownItem
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return Dropdown(position, label, content, items, out T selected, focusType) ? selected : item;
        }

        public static T Dropdown<T>(Rect position, GUIContent label, GUIContent content, IEnumerable<T> items, T item, FocusType focusType = FocusType.Keyboard) where T : DropdownItem
        {
            return Dropdown(position, label, content, items, out T selected, focusType) ? selected : item;
        }

        public static bool Dropdown<T>(Rect position, GUIContent label, GUIContent content, IEnumerable<T> items, out T item, FocusType focusType = FocusType.Keyboard) where T : DropdownItem
        {
            Dropdown(position, label, content, items, out int controlId, focusType);

            if (DropdownEditorUtility.TryGetDropdownSelection(controlId, out item))
            {
                DropdownEditorUtility.ClearDropdownSelection<T>();
                return true;
            }

            item = default;
            return false;
        }

        public static bool Dropdown<T>(Rect position, GUIContent label, GUIContent content, IEnumerable<T> items, out int controlId, FocusType focusType = FocusType.Keyboard) where T : DropdownItem
        {
            if (DropdownButton(position, label, content, out Rect dropdownPosition, focusType))
            {
                DropdownHandler<T> handler = DropdownEditorUtility.GetDropdownHandler<T>();

                controlId = EditorIMGUIUtility.GetLastControlId();

                handler.Show(dropdownPosition, controlId, items);

                return true;
            }

            controlId = EditorIMGUIUtility.GetLastControlId();
            return false;
        }

        public static bool DropdownButton(GUIContent label, GUIContent content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DropdownButton(position, label, content, out dropdownPosition, focusType);
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
