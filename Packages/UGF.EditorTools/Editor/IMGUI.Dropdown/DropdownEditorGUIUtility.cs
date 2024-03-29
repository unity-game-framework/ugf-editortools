﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorGUIUtility
    {
        public static bool Dropdown<T>(GUIContent label, GUIContent content, DropdownSelection<T> selection, Func<IEnumerable<T>> itemsHandler, out T item, FocusType focusType = FocusType.Keyboard, GUIStyle style = null, params GUILayoutOption[] options) where T : DropdownItem
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return Dropdown(position, label, content, selection, itemsHandler, out item, focusType, style);
        }

        public static bool Dropdown<T>(Rect position, GUIContent label, GUIContent content, DropdownSelection<T> selection, Func<IEnumerable<T>> itemsHandler, out T item, FocusType focusType = FocusType.Keyboard, GUIStyle style = null) where T : DropdownItem
        {
            if (itemsHandler == null) throw new ArgumentNullException(nameof(itemsHandler));

            bool result = DropdownButton(position, label, content, out Rect dropdownPosition, focusType, style);

            return ProcessDropdown(dropdownPosition, result, selection, itemsHandler, out item);
        }

        public static bool ProcessDropdown<T>(Rect position, bool show, DropdownSelection<T> selection, Func<IEnumerable<T>> itemsHandler, out T item) where T : DropdownItem
        {
            int controlId = EditorIMGUIUtility.GetLastControlId();

            return ProcessDropdown(position, show, controlId, selection, itemsHandler, out item);
        }

        public static bool ProcessDropdown<T>(Rect position, bool show, int controlId, DropdownSelection<T> selection, Func<IEnumerable<T>> itemsHandler, out T item) where T : DropdownItem
        {
            if (show)
            {
                IEnumerable<T> items = itemsHandler();

                ShowDropdown(controlId, position, selection, items);
            }

            return CheckDropdown(controlId, selection, out item);
        }

        public static void ShowDropdown<T>(int controlId, Rect position, DropdownSelection<T> selection, IEnumerable<T> items) where T : DropdownItem
        {
            if (selection == null) throw new ArgumentNullException(nameof(selection));

            selection.Show(position, controlId, items);
        }

        public static bool CheckDropdown<T>(int controlId, DropdownSelection<T> selection, out T item) where T : DropdownItem
        {
            if (selection == null) throw new ArgumentNullException(nameof(selection));

            if (selection.TryGet(controlId, out item))
            {
                selection.Clear();

                GUIUtility.keyboardControl = controlId;

                return true;
            }

            item = default;
            return false;
        }

        public static bool DropdownButton(GUIContent label, GUIContent content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, GUIStyle style = null, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DropdownButton(position, label, content, out dropdownPosition, focusType, style);
        }

        public static bool DropdownButton(Rect position, GUIContent label, GUIContent content, out Rect dropdownPosition, FocusType focusType = FocusType.Keyboard, GUIStyle style = null)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            style ??= EditorStyles.miniPullDown;

            if (label != GUIContent.none)
            {
                position = EditorGUI.PrefixLabel(position, label);
            }

            dropdownPosition = position;

            return EditorGUI.DropdownButton(position, content, focusType, style);
        }
    }
}
