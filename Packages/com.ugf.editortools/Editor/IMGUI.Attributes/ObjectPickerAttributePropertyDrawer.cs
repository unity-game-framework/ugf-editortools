using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(ObjectPickerAttribute), true)]
    internal class ObjectPickerAttributePropertyDrawer : PropertyDrawerTyped<ObjectPickerAttribute>
    {
        public ObjectPickerAttributePropertyDrawer() : base(SerializedPropertyType.ObjectReference)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawObjectPickerField(position, label, serializedProperty, Attribute.TargetType, Attribute.Filter);
        }
    }
}
