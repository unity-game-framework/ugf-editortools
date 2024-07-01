using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(TimeSpanTicksAttribute), true)]
    internal class TimeSpanTicksAttributePropertyDrawer : PropertyDrawerTyped<TimeSpanTicksAttribute>
    {
        public TimeSpanTicksAttributePropertyDrawer() : base(SerializedPropertyType.Integer)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorElementsUtility.TimeSpanTicksField(position, label, serializedProperty);
        }
    }
}
