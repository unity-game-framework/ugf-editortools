using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorObjectReferenceElement : EditorInspectorElement
    {
        public SerializedProperty SerializedProperty { get; }

        public EditorObjectReferenceElement(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));

            RegisterCallback<AttachToPanelEvent>(OnAttach);

            this.TrackPropertyValue(serializedProperty, OnChange);
        }

        private void OnAttach(AttachToPanelEvent attachToPanelEvent)
        {
            OnUpdate();
        }

        private void OnChange(SerializedProperty serializedProperty)
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            if (SerializedProperty.objectReferenceValue != null && SerializedProperty.objectReferenceValue != SerializedProperty.serializedObject.targetObject)
            {
                SetTarget(SerializedProperty.objectReferenceValue);
            }
            else
            {
                ClearTarget();
            }
        }
    }
}
