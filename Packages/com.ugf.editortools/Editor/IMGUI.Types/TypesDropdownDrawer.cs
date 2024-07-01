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
            Selection.Dropdown.RootName = "Types";
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

                if (type != null)
                {
                    string name = OnGetContentDisplayText(serializedProperty, type);

                    content = new GUIContent(name);
                }
                else
                {
                    content = ContentMissing;
                }
            }
            else
            {
                content = ContentNone;
            }

            return content;
        }

        protected virtual string OnGetContentDisplayText(SerializedProperty serializedProperty, Type type)
        {
            return TypesDropdownEditorUtility.GetTypeDisplayName(type, false);
        }
    }
}
