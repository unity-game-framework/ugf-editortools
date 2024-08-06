using System;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.SerializedProperties;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Ids
{
    public class GlobalIdFieldElement : TextField
    {
        public SerializedPropertyFieldBinding<string> PropertyBinding { get; }
        public GlobalId IdValue { get { return GlobalId.TryParse(value, out GlobalId id) ? id : GlobalId.Empty; } set { base.value = value.ToString(); } }

        public GlobalIdFieldElement(SerializedProperty serializedProperty, bool field = false) : this()
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (field)
            {
                UIToolkitEditorUtility.AddFieldClasses(this);
            }

            PropertyBinding.Bind(serializedProperty);

            this.TrackPropertyValue(serializedProperty);
        }

        public GlobalIdFieldElement()
        {
            PropertyBinding = new SerializedPropertyFieldBinding<string>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            if (!serializedProperty.hasMultipleDifferentValues)
            {
                IdValue = GlobalIdEditorUtility.GetGlobalIdFromProperty(serializedProperty);
            }
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            GlobalIdEditorUtility.SetGlobalIdToProperty(serializedProperty, IdValue);

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
