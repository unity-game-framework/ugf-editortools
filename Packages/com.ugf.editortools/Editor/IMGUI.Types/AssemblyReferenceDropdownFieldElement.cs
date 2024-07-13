using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class AssemblyReferenceDropdownFieldElement : DropdownFieldElement<string>
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public bool DisplayFullPath { get; set; } = true;
        public string ContentValueMissingLabel { get; set; } = "Missing";

        public DropdownItem<string> NoneItem { get; set; } = new DropdownItem<string>("None", "")
        {
            Priority = int.MaxValue
        };

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

            Items.Add(NoneItem);

            AssemblyDropdownEditorUtility.GetAssemblyItems(Items, DisplayFullPath);
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
    }
}
