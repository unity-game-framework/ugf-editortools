using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public class ManagedReferenceDropdownDrawer : DropdownDrawer<DropdownItem<Type>>
    {
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public ManagedReferenceDropdownDrawer(Func<IEnumerable<DropdownItem<Type>>> itemsHandler, DropdownSelection<DropdownItem<Type>> selection = null) : base(itemsHandler, selection)
        {
            Selection.Dropdown.RootName = "Types";
        }

        public override void DrawGUI(Rect position, GUIContent label, SerializedProperty serializedProperty, FocusType focusType = FocusType.Keyboard)
        {
            Rect dropdownPosition = position;

            float labelWidth = EditorGUIUtility.labelWidth;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            dropdownPosition.x = position.x + labelWidth + space;
            dropdownPosition.width = position.width - labelWidth - space;
            dropdownPosition.height = height;

            base.DrawGUI(dropdownPosition, GUIContent.none, serializedProperty, focusType);

            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                EditorGUI.PropertyField(position, serializedProperty, label, true);
            }
        }

        protected override void OnApplySelected(SerializedProperty serializedProperty, DropdownItem<Type> selected)
        {
            serializedProperty.managedReferenceValue = selected.Value != null ? Activator.CreateInstance(selected.Value) : null;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected override GUIContent OnGetContentLabel(SerializedProperty serializedProperty)
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
