using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdownDrawer
    {
        public IReadOnlyList<DropdownItem<Type>> Items { get; }
        public DropdownSelection<DropdownItem<Type>> Selection { get; }
        public DropdownItem<Type> ItemNone { get; set; } = new DropdownItem<Type>("None");
        public GUIContent ContentNone { get; set; } = new GUIContent("None");
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public TypesDropdownDrawer(IReadOnlyList<DropdownItem<Type>> items = null, DropdownSelection<DropdownItem<Type>> selection = null)
        {
            Items = items ?? new List<DropdownItem<Type>>();
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
            DropdownItem<Type> selected = GetSelectedItem(serializedProperty);
            GUIContent content = GetContentLabel(serializedProperty);

            selected = DropdownEditorGUIUtility.Dropdown(position, label, content, Selection, Items, selected, focusType);

            if (selected != ItemNone && serializedProperty.stringValue != selected.Value.AssemblyQualifiedName)
            {
                serializedProperty.stringValue = selected.Value.AssemblyQualifiedName;
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

        private DropdownItem<Type> GetSelectedItem(SerializedProperty serializedProperty)
        {
            string value = serializedProperty.stringValue;

            for (int i = 0; i < Items.Count; i++)
            {
                DropdownItem<Type> item = Items[i];
                string itemValue = item.Value.AssemblyQualifiedName;

                if (itemValue == value)
                {
                    return item;
                }
            }

            return ItemNone;
        }
    }
}
