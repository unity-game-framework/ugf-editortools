using System;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class PropertyBindObjectFieldElement : ObjectField
    {
        public SerializedPropertyFieldBinding<Object> PropertyBinding { get; }

        public PropertyBindObjectFieldElement(SerializedProperty serializedProperty, bool field = false, bool addContextMenu = true) : this(addContextMenu)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public PropertyBindObjectFieldElement(bool addContextMenu = true)
        {
            PropertyBinding = new SerializedPropertyFieldBinding<Object>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;

            if (addContextMenu)
            {
                this.AddManipulator(new ContextualMenuManipulator(OnContextMenuPopulate));
            }
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnUpdate(serializedProperty);
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            OnApply(serializedProperty);

            serializedProperty.serializedObject.ApplyModifiedProperties();
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

        protected virtual void OnContextMenuPopulate(ContextualMenuPopulateEvent populateEvent)
        {
            UIToolkitEditorUtility.AddObjectFieldContextMenuActions(this, populateEvent);
        }
    }
}
