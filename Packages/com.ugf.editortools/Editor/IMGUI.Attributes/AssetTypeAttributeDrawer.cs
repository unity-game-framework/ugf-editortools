using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(AssetTypeAttribute), true)]
    internal class AssetTypeAttributeDrawer : PropertyDrawerTyped<AssetTypeAttribute>
    {
        public AssetTypeAttributeDrawer() : base(SerializedPropertyType.ObjectReference)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.ObjectField(position, property, Attribute.AssetType, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyBindObjectFieldElement(property, true, false)
            {
                label = preferredLabel,
                objectType = Attribute.AssetType,
                bindingPath = property.propertyPath
            };
        }
    }
}
