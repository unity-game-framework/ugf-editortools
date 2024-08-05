using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ListKeyAndValueElement : ListElement
    {
        public string PropertyKeyName { get; }
        public string PropertyValueName { get; }
        public bool DisplayLabels { get; set; }
        public string KeyLabel { get; set; }
        public string ValueLabel { get; set; }

        public ListKeyAndValueElement(SerializedProperty serializedProperty, bool field, string propertyKeyName = "m_key", string propertyValueName = "m_value") : base(serializedProperty, field)
        {
            if (string.IsNullOrEmpty(propertyKeyName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyKeyName));
            if (string.IsNullOrEmpty(propertyValueName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyValueName));

            PropertyKeyName = propertyKeyName;
            PropertyValueName = propertyValueName;

            makeItem = OnCreateItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;
        }

        public ListKeyAndValueElement(string propertyKeyName = "m_key", string propertyValueName = "m_value")
        {
            if (string.IsNullOrEmpty(propertyKeyName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyKeyName));
            if (string.IsNullOrEmpty(propertyValueName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyValueName));

            PropertyKeyName = propertyKeyName;
            PropertyValueName = propertyValueName;

            makeItem = OnCreateItem;
            destroyItem = OnDestroyItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;
        }

        private VisualElement OnCreateItem()
        {
            return new KeyAndValueFieldElement(new PropertyField(), new PropertyField());
        }

        private void OnDestroyItem(VisualElement element)
        {
        }

        private void OnBindItem(VisualElement element, int index)
        {
            if (HasSerializedProperty)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty propertyKey = propertyElement.FindPropertyRelative(PropertyKeyName);
                SerializedProperty propertyValue = propertyElement.FindPropertyRelative(PropertyValueName);

                var fieldElement = (KeyAndValueFieldElement)element;
                var keyElement = (PropertyField)fieldElement.KeyElement;
                var valueElement = (PropertyField)fieldElement.ValueElement;

                if (DisplayLabels)
                {
                    keyElement.label = !string.IsNullOrEmpty(KeyLabel) ? KeyLabel : propertyKey.displayName;
                    valueElement.label = !string.IsNullOrEmpty(ValueLabel) ? ValueLabel : propertyValue.displayName;
                }
                else
                {
                    keyElement.label = string.Empty;
                    valueElement.label = string.Empty;
                }

                keyElement.bindingPath = propertyKey.propertyPath;
                valueElement.bindingPath = propertyValue.propertyPath;

                keyElement.BindProperty(propertyKey);
                valueElement.BindProperty(propertyValue);
            }
        }

        private void OnUnbindItem(VisualElement element, int index)
        {
            if (HasSerializedProperty)
            {
                var fieldElement = (KeyAndValueFieldElement)element;
                var keyElement = (PropertyField)fieldElement.KeyElement;
                var valueElement = (PropertyField)fieldElement.ValueElement;

                keyElement.Unbind();
                valueElement.Unbind();

                keyElement.bindingPath = null;
                valueElement.bindingPath = null;

                keyElement.label = null;
                valueElement.label = null;
            }
        }
    }
}
