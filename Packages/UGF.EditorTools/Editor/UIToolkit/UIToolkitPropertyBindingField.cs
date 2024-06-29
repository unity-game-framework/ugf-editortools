using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public static class UIToolkitPropertyBindingField<TValue>
    {
        private static readonly EventCallback<AttachToPanelEvent, (SerializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue>)> m_callbackAttach;
        private static readonly EventCallback<ChangeEvent<TValue>, (SerializedProperty, UIToolkitPropertyBindingFieldApplyHandler<TValue>)> m_callbackChange;
        private static readonly EventCallback<SerializedPropertyChangeEvent, (SerializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue>)> m_callbackPropertyChange;

        static UIToolkitPropertyBindingField()
        {
            m_callbackAttach = OnAttach;
            m_callbackChange = OnChange;
            m_callbackPropertyChange = OnPropertyChange;
        }

        public static void Bind(BaseField<TValue> field, SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue> updateHandler, UIToolkitPropertyBindingFieldApplyHandler<TValue> applyHandler)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (updateHandler == null) throw new ArgumentNullException(nameof(updateHandler));
            if (applyHandler == null) throw new ArgumentNullException(nameof(applyHandler));

            field.RegisterCallback(m_callbackAttach, (serializedProperty, updateHandler));
            field.RegisterCallback(m_callbackChange, (serializedProperty, applyHandler));
            field.RegisterCallback(m_callbackPropertyChange, (serializedProperty, updateHandler));
            field.TrackPropertyValue(serializedProperty);
        }

        public static void Unbind(BaseField<TValue> field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            field.UnregisterCallback(m_callbackAttach);
            field.UnregisterCallback(m_callbackChange);
            field.UnregisterCallback(m_callbackPropertyChange);
        }

        private static void OnUpdate(BaseField<TValue> field, SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue> handler)
        {
            field.showMixedValue = serializedProperty.hasMultipleDifferentValues;

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                field.value = handler.Invoke(field, serializedProperty);
            }
        }

        private static void OnApply(BaseField<TValue> field, SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldApplyHandler<TValue> handler, TValue value)
        {
            field.value = handler.Invoke(field, serializedProperty, value);

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private static void OnAttach(AttachToPanelEvent elementEvent, (SerializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue>) arguments)
        {
            var field = (BaseField<TValue>)elementEvent.target;
            (SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue> handler) = arguments;

            OnUpdate(field, serializedProperty, handler);
        }

        private static void OnChange(ChangeEvent<TValue> elementEvent, (SerializedProperty, UIToolkitPropertyBindingFieldApplyHandler<TValue>) arguments)
        {
            var field = (BaseField<TValue>)elementEvent.target;
            (SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldApplyHandler<TValue> handler) = arguments;

            OnApply(field, serializedProperty, handler, elementEvent.newValue);
        }

        private static void OnPropertyChange(SerializedPropertyChangeEvent elementEvent, (SerializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue>) arguments)
        {
            var field = (BaseField<TValue>)elementEvent.target;
            (SerializedProperty serializedProperty, UIToolkitPropertyBindingFieldUpdateHandler<TValue> handler) = arguments;

            OnUpdate(field, serializedProperty, handler);
        }
    }
}
