using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public class ManagedReferenceDropdownDrawer : DropdownDrawer<DropdownItem<Type>>
    {
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ButtonContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("_Menu"));
            public GUIStyle ButtonStyle { get; } = new GUIStyle("IconButton");
        }

        public ManagedReferenceDropdownDrawer(Func<IEnumerable<DropdownItem<Type>>> itemsHandler, DropdownSelection<DropdownItem<Type>> selection = null) : base(itemsHandler, selection)
        {
            Selection.Dropdown.RootName = "Current: None";
            Selection.Dropdown.MinimumWidth = 250F;
        }

        public override void DrawGUI(Rect position, GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard)
        {
            if (m_styles == null)
            {
                m_styles = new Styles();
            }

            Rect dropdownPosition = position;
            float height = EditorGUIUtility.singleLineHeight;

            dropdownPosition.x = position.xMax - height;
            dropdownPosition.width = height;
            dropdownPosition.height = height;

            bool result = GUI.Button(dropdownPosition, m_styles.ButtonContent, m_styles.ButtonStyle);

            if (result)
            {
                Selection.Dropdown.RootName = GetCurrentTypeTitle(serializedProperty);
            }

            if (DropdownEditorGUIUtility.ProcessDropdown(dropdownPosition, result, Selection, ItemsHandler, out DropdownItem<Type> selected))
            {
                OnApplySelected(serializedProperty, selected);
            }
        }

        protected override void OnApplySelected(SerializedProperty serializedProperty, DropdownItem<Type> selected)
        {
            serializedProperty.managedReferenceValue = selected.Value != null ? Activator.CreateInstance(selected.Value) : null;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private string GetCurrentTypeTitle(SerializedProperty serializedProperty)
        {
            string title;

            if (!string.IsNullOrEmpty(serializedProperty.managedReferenceFullTypename))
            {
                title = ManagedReferenceEditorUtility.TryGetType(serializedProperty.managedReferenceFullTypename, out Type type) ? $"Current: {type.Name}" : "Current: Missing";
            }
            else
            {
                title = "Current: None";
            }

            return title;
        }
    }
}
