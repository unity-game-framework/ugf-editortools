using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(TimeTicksAttribute), true)]
    internal class TimeTicksAttributePropertyDrawer : PropertyDrawerTyped<TimeTicksAttribute>
    {
        public TimeTicksAttributePropertyDrawer() : base(SerializedPropertyType.Integer)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorElementsUtility.TimeTicksField(position, label, serializedProperty);
        }
    }
}
