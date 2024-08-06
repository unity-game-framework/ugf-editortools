using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(ListAttribute), true)]
    internal class ListAttributePropertyDrawer : PropertyDrawerTyped<ListAttribute>
    {
        public ListAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            OnDrawPropertyDefault(position, serializedProperty, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new ListElement(property, true)
            {
                headerTitle = preferredLabel
            };
        }
    }
}
