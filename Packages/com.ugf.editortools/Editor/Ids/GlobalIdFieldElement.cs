using System;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Ids
{
    public class GlobalIdFieldElement : TextField
    {
        public UIToolkitPropertyBindingField<string> PropertyBinding { get; }
        public GlobalId IdValue { get { return GlobalId.TryParse(value, out GlobalId id) ? id : GlobalId.Empty; } set { base.value = value.ToString(); } }

        public GlobalIdFieldElement()
        {
            PropertyBinding = new UIToolkitPropertyBindingField<string>(this);
            PropertyBinding.Update += Update;
            PropertyBinding.Apply += Apply;
        }

        public void Update(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            IdValue = GlobalIdEditorUtility.GetGlobalIdFromProperty(serializedProperty);
        }

        public void Apply(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            GlobalIdEditorUtility.SetGlobalIdToProperty(serializedProperty, IdValue);

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
