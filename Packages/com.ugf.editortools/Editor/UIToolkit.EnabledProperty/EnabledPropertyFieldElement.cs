using System;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.EnabledProperty
{
    public class EnabledPropertyFieldElement : BaseField<bool>
    {
        public SerializedPropertyFieldBinding<bool> PropertyBinding { get; }
        public VisualElement ValueElement { get; }
        public Toggle ToggleElement { get; }

        public static string UssClassName { get; } = "ugf-enabled-property";
        public static string ToggleUssClassName { get; } = "ugf-enabled-property__toggle";
        public static string ToggleElementName { get; } = "enabled-property-toggle";

        public EnabledPropertyFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            SerializedProperty propertyEnabled = serializedProperty.FindPropertyRelative("m_enabled");
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            var propertyFieldElement = new PropertyField(propertyValue);

            propertyFieldElement.AddToClassList(inputUssClassName);

            if (propertyValue.hasVisibleChildren)
            {
                propertyFieldElement.style.paddingLeft = EditorGUIUtility.singleLineHeight;
            }

            ToggleElement.bindingPath = propertyEnabled.propertyPath;
            ValueElement.Add(propertyFieldElement);

            bindingPath = serializedProperty.propertyPath;

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public EnabledPropertyFieldElement() : base(null, null)
        {
            PropertyBinding = new SerializedPropertyFieldBinding<bool>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            ValueElement = this.Query(className: inputUssClassName).First();
            ValueElement.AddToClassList(BaseCompositeField<int, IntegerField, int>.inputUssClassName);

            ToggleElement = new Toggle
            {
                name = ToggleElementName
            };

            ToggleElement.AddToClassList(ToggleUssClassName);
            ToggleElement.AddToClassList(BaseCompositeField<int, IntegerField, int>.firstFieldVariantUssClassName);
            ToggleElement.RegisterValueChangedCallback(OnToggle);

            ValueElement.Add(ToggleElement);

            AddToClassList(UssClassName);
            AddToClassList(BaseCompositeField<int, IntegerField, int>.ussClassName);

            UIToolkitEditorUtility.AddStyleSheets(this);
        }

        public override void SetValueWithoutNotify(bool newValue)
        {
            base.SetValueWithoutNotify(newValue);

            OnUpdateValueState(newValue);
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyEnabled = serializedProperty.FindPropertyRelative("m_enabled");

                value = propertyEnabled.boolValue;

                if (!value)
                {
                    OnUpdateValueState(false);
                }
            }
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyEnabled = serializedProperty.FindPropertyRelative("m_enabled");

                propertyEnabled.boolValue = value;
            }
        }

        public void SetValueElementsEnabled(bool enabled)
        {
            foreach (VisualElement visualElement in ValueElement.Children())
            {
                if (visualElement.name != ToggleElementName)
                {
                    visualElement.enabledSelf = enabled;
                }
            }
        }

        private void OnUpdateValueState(bool enabled)
        {
            SetValueElementsEnabled(enabled);

            ToggleElement.SetValueWithoutNotify(enabled);
        }

        private void OnToggle(ChangeEvent<bool> changeEvent)
        {
            value = changeEvent.newValue;
        }
    }
}
