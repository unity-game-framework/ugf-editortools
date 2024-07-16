using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEditor.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class AssemblyReferenceDropdownFieldElement : DropdownFieldElement<string, DropdownItem<Assembly>>
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public DropdownItem<Assembly> ItemNone { get; set; }
        public string ContentValueMissingLabel { get; set; } = "Missing";
        public bool DisplayFullPath { get; set; } = true;

        public AssemblyReferenceDropdownFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            bindingPath = propertyValue.propertyPath;

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public AssemblyReferenceDropdownFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<string>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            Selection.Dropdown.RootName = "Assemblies";

            ItemNone = new DropdownItem<Assembly>(ContentValueNoneLabel)
            {
                Priority = int.MaxValue
            };
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

                value = propertyValue.stringValue;
            }
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

                propertyValue.stringValue = value;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected override void OnSelected(DropdownItem<Assembly> item)
        {
            base.OnSelected(item);

            value = item.Value?.FullName ?? string.Empty;
        }

        protected override void OnUpdateValueContent()
        {
            base.OnUpdateValueContent();

            if (!string.IsNullOrEmpty(value))
            {
                ButtonElement.text = AssemblyUtility.TryGetAssemblyByFullName(value, out Assembly assembly)
                    ? assembly.GetName().Name
                    : ContentValueMissingLabel;
            }
        }

        protected override IEnumerable<DropdownItem<Assembly>> OnGetItems()
        {
            var items = new List<DropdownItem<Assembly>> { ItemNone };

            AssemblyDropdownEditorUtility.GetAssemblyItems(items, DisplayFullPath);

            return items;
        }
    }
}
