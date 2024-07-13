using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class AssemblyReferenceDropdownFieldElement : DropdownFieldElement<Assembly>
    {
        public bool DisplayFullPath { get; set; } = true;
        public string ContentValueMissingLabel { get; set; } = "Missing";

        public DropdownItem<Assembly> NoneItem { get; set; } = new DropdownItem<Assembly>("None")
        {
            Priority = int.MaxValue
        };

        public AssemblyReferenceDropdownFieldElement(SerializedProperty serializedProperty, bool field) : base(serializedProperty, field)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            bindingPath = propertyValue.propertyPath;

            Selection.Dropdown.RootName = "Assemblies";

            Items.Add(NoneItem);

            AssemblyDropdownEditorUtility.GetAssemblyItems(Items, DisplayFullPath);
        }

        public AssemblyReferenceDropdownFieldElement()
        {
            Selection.Dropdown.RootName = "Assemblies";

            Items.Add(NoneItem);

            AssemblyDropdownEditorUtility.GetAssemblyItems(Items, DisplayFullPath);
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            base.OnUpdate(serializedProperty);

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

                if (!string.IsNullOrEmpty(propertyValue.stringValue)
                    && AssemblyUtility.TryGetAssemblyByFullName(propertyValue.stringValue, out Assembly assembly))
                {
                    value = assembly;
                }
                else
                {
                    value = null;
                }
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            base.OnApply(serializedProperty);

            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            propertyValue.stringValue = value != null ? value.FullName : string.Empty;
            propertyValue.serializedObject.ApplyModifiedProperties();
        }

        protected override void OnUpdateValueContent()
        {
            if (PropertyBinding.HasSerializedProperty
                && value == null)
            {
                SerializedProperty propertyValue = PropertyBinding.SerializedProperty.FindPropertyRelative("m_value");

                if (!string.IsNullOrEmpty(propertyValue.stringValue))
                {
                    ButtonElement.text = ContentValueMissingLabel;
                }
                else
                {
                    base.OnUpdateValueContent();
                }
            }
            else
            {
                base.OnUpdateValueContent();
            }
        }
    }
}
