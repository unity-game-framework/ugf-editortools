using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public class ManagedReferenceDropdownDrawer
    {
        public Func<IEnumerable<DropdownItem<Type>>> ItemsHandler { get; }
        public DropdownSelection<DropdownItem<Type>> Selection { get; }
        public GUIContent ContentNone { get; set; } = new GUIContent("None");
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public ManagedReferenceDropdownDrawer(Func<IEnumerable<DropdownItem<Type>>> itemsHandler, DropdownSelection<DropdownItem<Type>> selection = null)
        {
            ItemsHandler = itemsHandler ?? throw new ArgumentNullException(nameof(itemsHandler));
            Selection = selection ?? new DropdownSelection<DropdownItem<Type>>(new Dropdown<DropdownItem<Type>>
            {
                RootName = "Types",
                MinimumHeight = 250F
            });
        }

        public void DrawGUILayout(GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawGUI(position, label, serializedProperty, focusType);
        }

        public void DrawGUI(Rect position, GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard)
        {
            Rect dropdownPosition = position;

            float labelWidth = EditorGUIUtility.labelWidth;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            dropdownPosition.x = position.x + labelWidth + space;
            dropdownPosition.width = position.width - labelWidth - space;
            dropdownPosition.height = height;

            GUIContent content = GetContentLabel(serializedProperty);

            if (DropdownEditorGUIUtility.Dropdown(dropdownPosition, GUIContent.none, content, Selection, ItemsHandler, out DropdownItem<Type> selected, focusType))
            {
                serializedProperty.managedReferenceValue = selected.Value != null ? Activator.CreateInstance(selected.Value) : null;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }

            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                EditorGUI.PropertyField(position, serializedProperty, label, true);
            }
        }

        private GUIContent GetContentLabel(SerializedProperty serializedProperty)
        {
            GUIContent content;

            if (!string.IsNullOrEmpty(serializedProperty.managedReferenceFullTypename))
            {
                content = ManagedReferenceEditorUtility.TryGetType(serializedProperty.managedReferenceFullTypename, out Type type) ? new GUIContent(type.Name) : ContentMissing;
            }
            else
            {
                content = ContentNone;
            }

            return content;
        }
    }
}
