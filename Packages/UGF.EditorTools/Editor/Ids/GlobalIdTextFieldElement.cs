using System;
using UGF.EditorTools.Editor.UIToolkit;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Ids
{
    public class GlobalIdTextFieldElement : TextField
    {
        public SerializedProperty SerializedProperty { get; }

        public GlobalIdTextFieldElement(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

            showMixedValue = SerializedProperty.hasMultipleDifferentValues;

            UIToolkitFieldUtility.SetClasses(this);

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);

            this.RegisterValueChangedCallback(OnValueChanged);
        }

        private void OnAttachToPanel(AttachToPanelEvent attachToPanelEvent)
        {
            value = GlobalIdEditorUtility.GetGuidFromProperty(SerializedProperty);
        }

        private void OnValueChanged(ChangeEvent<string> changeEvent)
        {
            GlobalIdEditorUtility.SetGuidToProperty(SerializedProperty, changeEvent.newValue);

            SerializedProperty.serializedObject.ApplyModifiedProperties();

            value = GlobalIdEditorUtility.GetGuidFromProperty(SerializedProperty);
        }
    }
}
