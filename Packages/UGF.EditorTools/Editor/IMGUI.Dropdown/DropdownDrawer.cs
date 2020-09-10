using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public abstract class DropdownDrawer<TItem> where TItem : DropdownItem
    {
        public Func<IEnumerable<TItem>> ItemsHandler { get; }
        public DropdownSelection<TItem> Selection { get; }
        public GUIContent ContentNone { get; set; } = new GUIContent("None");

        protected DropdownDrawer(Func<IEnumerable<TItem>> itemsHandler, DropdownSelection<TItem> selection = null)
        {
            ItemsHandler = itemsHandler ?? throw new ArgumentNullException(nameof(itemsHandler));
            Selection = selection ?? new DropdownSelection<TItem>(new Dropdown<TItem>
            {
                MinimumHeight = 250F
            });
        }

        public void DrawGUILayout(GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawGUI(position, label, serializedProperty, focusType);
        }

        public virtual void DrawGUI(Rect position, GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnDrawDropdown(position, label, serializedProperty, focusType);
        }

        protected virtual void OnDrawDropdown(Rect position, GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard)
        {
            GUIContent content = OnGetContentLabel(serializedProperty);

            if (DropdownEditorGUIUtility.Dropdown(position, label, content, Selection, ItemsHandler, out TItem selected, focusType))
            {
                OnApplySelected(serializedProperty, selected);
            }
        }

        protected virtual void OnApplySelected(SerializedProperty serializedProperty, TItem selected)
        {
        }

        protected virtual GUIContent OnGetContentLabel(SerializedProperty serializedProperty)
        {
            return ContentNone;
        }
    }
}
