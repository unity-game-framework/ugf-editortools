using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Ids
{
    public static class GlobalIdBindingTextField
    {
        private static readonly EventCallback<AttachToPanelEvent, SerializedProperty> m_callbackAttach;
        private static readonly EventCallback<ChangeEvent<string>, SerializedProperty> m_callbackChange;
        private static readonly EventCallback<SerializedPropertyChangeEvent, SerializedProperty> m_callbackPropertyChange;

        static GlobalIdBindingTextField()
        {
            m_callbackAttach = OnAttach;
            m_callbackChange = OnChange;
            m_callbackPropertyChange = OnPropertyChange;
        }

        public static void Bind(TextField textField, SerializedProperty serializedProperty)
        {
            if (textField == null) throw new ArgumentNullException(nameof(textField));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            textField.RegisterCallback(m_callbackAttach, serializedProperty);
            textField.RegisterCallback(m_callbackChange, serializedProperty);
            textField.RegisterCallback(m_callbackPropertyChange, serializedProperty);
            textField.TrackPropertyValue(serializedProperty);
        }

        public static void Update(TextField textField, SerializedProperty serializedProperty)
        {
            if (textField == null) throw new ArgumentNullException(nameof(textField));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            textField.showMixedValue = serializedProperty.hasMultipleDifferentValues;

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                textField.value = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);
            }
        }

        public static void Apply(TextField textField, SerializedProperty serializedProperty, string value)
        {
            if (textField == null) throw new ArgumentNullException(nameof(textField));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, value);

            serializedProperty.serializedObject.ApplyModifiedProperties();

            textField.value = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);
        }

        private static void OnAttach(AttachToPanelEvent elementEvent, SerializedProperty serializedProperty)
        {
            var element = (TextField)elementEvent.target;

            Update(element, serializedProperty);
        }

        private static void OnChange(ChangeEvent<string> elementEvent, SerializedProperty serializedProperty)
        {
            var element = (TextField)elementEvent.target;

            Apply(element, serializedProperty, elementEvent.newValue);
        }

        private static void OnPropertyChange(SerializedPropertyChangeEvent elementEvent, SerializedProperty serializedProperty)
        {
            var element = (TextField)elementEvent.target;

            Update(element, serializedProperty);
        }
    }
}
