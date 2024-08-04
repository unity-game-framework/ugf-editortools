using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(ListKeyAndValueAttribute), true)]
    internal class ListKeyAndValueAttributePropertyDrawer : PropertyDrawerTyped<ListKeyAndValueAttribute>
    {
        public ListKeyAndValueAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            OnDrawPropertyDefault(position, serializedProperty, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new ListKeyAndValueElement(property, true, Attribute.PropertyKeyName, Attribute.PropertyValueName)
            {
                headerTitle = preferredLabel,
                DisplayLabels = Attribute.DisplayLabels,
                KeyLabel = Attribute.KeyLabel,
                ValueLabel = Attribute.ValueLabel
            };
        }
    }
}
