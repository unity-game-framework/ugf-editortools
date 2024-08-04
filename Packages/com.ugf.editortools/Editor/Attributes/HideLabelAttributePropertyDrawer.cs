using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(HideLabelAttribute), true)]
    internal class HideLabelAttributePropertyDrawer : PropertyDrawer<HideLabelAttribute>
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property)
            {
                label = string.Empty
            };
        }
    }
}
