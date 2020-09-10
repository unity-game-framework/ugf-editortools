using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdownDrawer : DropdownDrawer<DropdownItem<Type>>
    {
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public TypesDropdownDrawer(Func<IEnumerable<DropdownItem<Type>>> itemsHandler, DropdownSelection<DropdownItem<Type>> selection = null) : base(itemsHandler, selection)
        {
        }

        protected override void OnApplySelected(SerializedProperty serializedProperty, DropdownItem<Type> selected)
        {
            base.OnApplySelected(serializedProperty, selected);

            serializedProperty.stringValue = selected.Value != null ? selected.Value.AssemblyQualifiedName : string.Empty;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected override GUIContent OnGetContentLabel(SerializedProperty serializedProperty)
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
