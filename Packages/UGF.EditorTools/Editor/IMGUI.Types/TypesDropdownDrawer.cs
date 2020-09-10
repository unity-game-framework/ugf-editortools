using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdownDrawer
    {
        public Func<IEnumerable<DropdownItem<Type>>> ItemsHandler { get; }
        public DropdownSelection<DropdownItem<Type>> Selection { get; }
        public GUIContent ContentNone { get; set; } = new GUIContent("None");
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public TypesDropdownDrawer(Func<IEnumerable<DropdownItem<Type>>> itemsHandler, DropdownSelection<DropdownItem<Type>> selection = null)
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
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            GUIContent content = GetContentLabel(serializedProperty);

            if (DropdownEditorGUIUtility.Dropdown(position, label, content, Selection, ItemsHandler, out DropdownItem<Type> selected, focusType))
            {
                serializedProperty.stringValue = selected.Value != null ? selected.Value.AssemblyQualifiedName : string.Empty;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        private GUIContent GetContentLabel(SerializedProperty serializedProperty)
        {
            GUIContent content;
            string value = serializedProperty.stringValue;

            if (!string.IsNullOrEmpty(value))
            {
                var type = Type.GetType(value);

                content = type != null ? new GUIContent(type.Name) : ContentMissing;
            }
            else
            {
                content = ContentNone;
            }

            return content;
        }
    }
}
