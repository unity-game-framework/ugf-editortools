using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class AssemblyDropdownDrawer : DropdownDrawer<DropdownItem<Assembly>>
    {
        public GUIContent ContentMissing { get; set; } = new GUIContent("Missing");

        public AssemblyDropdownDrawer(Func<IEnumerable<DropdownItem<Assembly>>> itemsHandler, DropdownSelection<DropdownItem<Assembly>> selection = null) : base(itemsHandler, selection)
        {
            Selection.Dropdown.RootName = "Assemblies";
        }

        protected override void OnApplySelected(SerializedProperty serializedProperty, DropdownItem<Assembly> selected)
        {
            base.OnApplySelected(serializedProperty, selected);

            serializedProperty.stringValue = selected.Value != null ? selected.Value.FullName : string.Empty;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        protected override GUIContent OnGetContentLabel(SerializedProperty serializedProperty)
        {
            GUIContent content;
            string value = serializedProperty.stringValue;

            if (!string.IsNullOrEmpty(value))
            {
                if (AssemblyUtility.TryGetAssemblyByFullName(value, out Assembly assembly))
                {
                    string name = OnGetContentDisplayText(serializedProperty, assembly);

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

        protected virtual string OnGetContentDisplayText(SerializedProperty serializedProperty, Assembly assembly)
        {
            return assembly.GetName().Name;
        }
    }
}
