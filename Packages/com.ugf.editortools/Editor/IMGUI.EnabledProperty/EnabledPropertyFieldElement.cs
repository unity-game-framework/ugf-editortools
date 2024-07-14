﻿using System;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.EnabledProperty
{
    public class EnabledPropertyFieldElement : BaseField<bool>
    {
        public SerializedPropertyFieldBinding<bool> PropertyBinding { get; }
        public VisualElement ValueElement { get; }
        public Toggle ToggleElement { get; }

        public static string ToggleElementName { get; } = "enabled-property-toggle";

        public EnabledPropertyFieldElement(SerializedProperty serializedProperty, bool field) : this()
        {
            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            var propertyFieldElement = new PropertyField(propertyValue);

            propertyFieldElement.AddToClassList(inputUssClassName);

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

            ToggleElement.RegisterValueChangedCallback(OnToggle);

            ValueElement.Add(ToggleElement);

            AddToClassList(BaseCompositeField<int, IntegerField, int>.ussClassName);
        }

        public override void SetValueWithoutNotify(bool newValue)
        {
            base.SetValueWithoutNotify(newValue);

            SetValueElementsEnabled(newValue);

            ToggleElement.SetValueWithoutNotify(newValue);
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                SerializedProperty propertyEnabled = serializedProperty.FindPropertyRelative("m_enabled");

                value = propertyEnabled.boolValue;
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

        private void OnToggle(ChangeEvent<bool> changeEvent)
        {
            value = changeEvent.newValue;
        }
    }
}
