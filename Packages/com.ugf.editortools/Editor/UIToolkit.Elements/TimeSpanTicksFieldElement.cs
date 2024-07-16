using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.UIToolkit.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class TimeSpanTicksFieldElement : DropdownButtonFieldElement<long>
    {
        public SerializedPropertyFieldBinding<long> PropertyBinding { get; }

        public TimeSpanTicksFieldElement(SerializedProperty serializedProperty, bool field = false) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            bindingPath = serializedProperty.propertyPath;

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public TimeSpanTicksFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<long>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;
            ButtonElement.clicked += OnDropdownButtonClicked;
        }

        public override void SetValueWithoutNotify(long newValue)
        {
            base.SetValueWithoutNotify(newValue);

            OnUpdateValueContent();
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                value = serializedProperty.longValue;
            }

            OnUpdateValueContent();
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            serializedProperty.longValue = value;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private void OnDropdownButtonClicked()
        {
            PopupWindow.Show(ButtonElement.worldBound, new TimeSpanTicksAttributePopupWindowContent(ButtonElement.worldBound, OnDropdownPopupClosed, value));
        }

        private void OnDropdownPopupClosed(long argument)
        {
            value = argument;
        }

        private void OnUpdateValueContent()
        {
            if (!showMixedValue)
            {
                string valueText = $@"{(value < 0 ? "-" : "")}{new TimeSpan(value):d\:hh\:mm\:ss\.fff}";

                ButtonElement.text = valueText;
            }
        }
    }
}
