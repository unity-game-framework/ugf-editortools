using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UGF.EditorTools.Editor.UIToolkit.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Types
{
    public class TypesDropdownFieldElement : DropdownFieldElement<string, DropdownItem<Type>>
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public DropdownItem<Type> ItemNone { get; set; }
        public string ContentValueMissingLabel { get; set; } = "Missing";
        public bool DisplayFullPath { get; set; } = true;
        public bool DisplayAssemblyName { get; set; }
        public Type TargetType { get; set; } = typeof(object);

        public TypesDropdownFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            bindingPath = serializedProperty.propertyPath;

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public TypesDropdownFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<string>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            Selection.Dropdown.RootName = "Types";

            ItemNone = new DropdownItem<Type>(ContentValueNoneLabel)
            {
                Priority = int.MaxValue
            };
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                value = serializedProperty.stringValue;
            }
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                serializedProperty.stringValue = value;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected override void OnSelected(DropdownItem<Type> item)
        {
            base.OnSelected(item);

            value = item.Value?.AssemblyQualifiedName ?? string.Empty;
        }

        protected override void OnUpdateValueContent()
        {
            base.OnUpdateValueContent();

            if (!string.IsNullOrEmpty(value))
            {
                var type = Type.GetType(value);

                ButtonElement.text = type != null
                    ? TypesDropdownEditorUtility.GetTypeDisplayName(type, false)
                    : ContentValueMissingLabel;
            }
        }

        protected override IEnumerable<DropdownItem<Type>> OnGetItems()
        {
            var items = new List<DropdownItem<Type>> { ItemNone };

            TypesDropdownEditorUtility.GetTypeItems(items, TargetType, DisplayFullPath, DisplayAssemblyName);

            return items;
        }
    }
}
