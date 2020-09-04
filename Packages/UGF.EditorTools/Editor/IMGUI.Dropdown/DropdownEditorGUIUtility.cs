using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorGUIUtility
    {
        private static readonly DropdownSelection<DropdownItem<object>> m_selection = new DropdownSelection<DropdownItem<object>>();

        public static DropdownItem<object> Dropdown(GUIContent label, GUIContent content, IEnumerable<DropdownItem<object>> items, DropdownItem<object> item, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return Dropdown(position, label, content, items, item, focusType);
        }

        public static DropdownItem<object> Dropdown(Rect position, GUIContent label, GUIContent content, IEnumerable<DropdownItem<object>> items, DropdownItem<object> item, FocusType focusType = FocusType.Keyboard)
        {
            return Dropdown(position, label, content, m_selection, items, out DropdownItem<object> selected, focusType) ? selected : item;
        }

        public static T Dropdown<T>(GUIContent label, GUIContent content, DropdownSelection<T> selection, IEnumerable<T> items, T item, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options) where T : DropdownItem
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return Dropdown(position, label, content, selection, items, item, focusType);
        }

        public static T Dropdown<T>(Rect position, GUIContent label, GUIContent content, DropdownSelection<T> selection, IEnumerable<T> items, T item, FocusType focusType = FocusType.Keyboard) where T : DropdownItem
        {
            return Dropdown(position, label, content, selection, items, out T selected, focusType) ? selected : item;
        }

        public static bool Dropdown<T>(Rect position, GUIContent label, GUIContent content, DropdownSelection<T> selection, IEnumerable<T> items, out T item, FocusType focusType = FocusType.Keyboard) where T : DropdownItem
        {
            int controlId;

            if (DropdownButton(position, label, content, out Rect dropdownPosition, focusType))
            {
                controlId = EditorIMGUIUtility.GetLastControlId();

                selection.Show(dropdownPosition, controlId, items);
            }
            else
            {
                controlId = EditorIMGUIUtility.GetLastControlId();
            }

            if (selection.TryGet(controlId, out item))
            {
                selection.Clear();

                GUIUtility.keyboardControl = controlId;

                return true;
            }

            item = default;
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
