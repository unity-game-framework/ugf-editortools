using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ReferenceObjectFieldElement : ObjectField
    {
        public UIToolkitPropertyBindingField<Object> PropertyBinding { get; }
        public IconButtonElement ReferenceButtonElement { get; }
        public static string ReferenceButtonUssClassName { get; } = "ugf-reference-object-field__button";
        public string ReferenceTooltipLabel { get; set; } = "Asset Path";
        public string ReferenceTooltipFormat { get; set; } = "{0}: {1}";
        public string ReferenceTooltipValueNone { get; set; } = "None";

        public ReferenceObjectFieldElement()
        {
            PropertyBinding = new UIToolkitPropertyBindingField<Object>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            ReferenceButtonElement = new IconButtonElement();
            ReferenceButtonElement.AddToClassList(ReferenceButtonUssClassName);
            ReferenceButtonElement.clicked += OnReferenceButtonClicked;

            VisualElement inputElement = this.Query<VisualElement>(className: inputUssClassName).First();
            VisualElement selectorElement = inputElement.Query<VisualElement>(className: selectorUssClassName).First();

            inputElement.Insert(inputElement.IndexOf(selectorElement), ReferenceButtonElement);

            UIToolkitEditorUtility.AddStyleSheets(this);
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnUpdate(serializedProperty);
            OnUpdateReferenceTooltip(serializedProperty);
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnApply(serializedProperty);

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        public void SetReferenceTooltipNone()
        {
            SetReferenceTooltip(ReferenceTooltipValueNone);
        }

        public void SetReferenceTooltipMixed()
        {
            SetReferenceTooltip(mixedValueString);
        }

        public void SetReferenceTooltip(string referenceValue)
        {
            if (string.IsNullOrEmpty(referenceValue)) throw new ArgumentException("Value cannot be null or empty.", nameof(referenceValue));

            ReferenceButtonElement.tooltip = string.Format(ReferenceTooltipFormat, ReferenceTooltipLabel, referenceValue);
        }

        protected virtual void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                value = serializedProperty.objectReferenceValue;
            }
        }

        protected virtual void OnApply(SerializedProperty serializedProperty)
        {
            serializedProperty.objectReferenceValue = value;
        }

        protected virtual void OnUpdateReferenceTooltip(SerializedProperty serializedProperty)
        {
            ReferenceButtonElement.enabledSelf = !serializedProperty.hasMultipleDifferentValues;

            if (serializedProperty.hasMultipleDifferentValues)
            {
                SetReferenceTooltipMixed();
            }
            else
            {
                string referenceValue = OnGetReferenceValueDisplay(serializedProperty);

                if (!string.IsNullOrEmpty(referenceValue))
                {
                    SetReferenceTooltip(referenceValue);
                }
                else
                {
                    SetReferenceTooltipNone();
                }
            }
        }

        protected virtual string OnGetReferenceValueDisplay(SerializedProperty serializedProperty)
        {
            return AssetDatabase.GetAssetPath(serializedProperty.objectReferenceValue);
        }

        protected virtual void OnReferenceButtonClicked()
        {
            if (PropertyBinding.HasSerializedProperty && !PropertyBinding.SerializedProperty.hasMultipleDifferentValues)
            {
                GUIUtility.systemCopyBuffer = OnGetReferenceValueDisplay(PropertyBinding.SerializedProperty);
            }
        }
    }
}
