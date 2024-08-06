using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.SerializedProperties
{
    public class SerializedPropertyFieldBinding<TValue>
    {
        public BaseField<TValue> FieldElement { get; }
        public SerializedProperty SerializedProperty { get { return m_serializedProperty ?? throw new ArgumentException("Value not specified."); } }
        public bool HasSerializedProperty { get { return m_serializedProperty != null; } }

        public event SerializedPropertyFieldBindingUpdateHandler Update;
        public event SerializedPropertyFieldBindingUpdateHandler Apply;

        private SerializedProperty m_serializedProperty;
        private EventCallback<AttachToPanelEvent> m_callbackAttach;
        private EventCallback<ChangeEvent<TValue>> m_callbackChange;
        private EventCallback<SerializedPropertyChangeEvent> m_callbackPropertyChange;

        public SerializedPropertyFieldBinding(BaseField<TValue> fieldElement)
        {
            FieldElement = fieldElement ?? throw new ArgumentNullException(nameof(fieldElement));
        }

        public void Bind(SerializedProperty serializedProperty)
        {
            m_serializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
            m_callbackAttach ??= OnAttach;
            m_callbackChange ??= OnChange;
            m_callbackPropertyChange ??= OnPropertyChange;

            FieldElement.RegisterCallback(m_callbackAttach);
            FieldElement.RegisterCallback(m_callbackChange);
            FieldElement.RegisterCallback(m_callbackPropertyChange);
        }

        public void Unbind()
        {
            if (!HasSerializedProperty) throw new InvalidOperationException("No property was bound.");

            FieldElement.UnregisterCallback(m_callbackAttach);
            FieldElement.UnregisterCallback(m_callbackChange);
            FieldElement.UnregisterCallback(m_callbackPropertyChange);

            m_serializedProperty = default;
        }

        private void OnUpdate()
        {
            if (HasSerializedProperty)
            {
                FieldElement.showMixedValue = SerializedProperty.hasMultipleDifferentValues;

                Update?.Invoke(SerializedProperty);
            }
        }

        private void OnApply()
        {
            if (HasSerializedProperty)
            {
                Apply?.Invoke(SerializedProperty);
            }
        }

        private void OnAttach(AttachToPanelEvent elementEvent)
        {
            OnUpdate();
        }

        private void OnChange(ChangeEvent<TValue> elementEvent)
        {
            OnApply();
        }

        private void OnPropertyChange(SerializedPropertyChangeEvent elementEvent)
        {
            OnUpdate();
        }
    }
}
