using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(SelectDirectoryAttribute), true)]
    internal class SelectDirectoryAttributePropertyDrawer : PropertyDrawerTyped<SelectDirectoryAttribute>
    {
        public SelectDirectoryAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawSelectDirectoryField(position, serializedProperty, label, Attribute.Title, Attribute.DefaultDirectory, Attribute.Relative);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new SelectDirectoryTextFieldElement(property, true)
            {
                label = preferredLabel,
                Title = Attribute.Title,
                DefaultDirectory = Attribute.DefaultDirectory,
                Relative = Attribute.Relative
            };
        }
    }
}
