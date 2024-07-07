using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(SelectFileAttribute), true)]
    internal class SelectFileAttributePropertyDrawer : PropertyDrawerTyped<SelectFileAttribute>
    {
        public SelectFileAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawSelectFileField(position, serializedProperty, label, Attribute.Title, Attribute.DefaultDirectory, Attribute.Extension, Attribute.InAssets);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new SelectFileTextFieldElement(property, true)
            {
                label = preferredLabel,
                Title = Attribute.Title,
                DefaultDirectory = Attribute.DefaultDirectory,
                Extension = Attribute.Extension,
                InAssets = Attribute.InAssets
            };
        }
    }
}
