using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(AssetGuidAttribute), true)]
    internal class AssetGuidAttributeDrawer : PropertyDrawerTyped<AssetGuidAttribute>
    {
        public AssetGuidAttributeDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawAssetGuidField(position, property, label, Attribute.AssetType);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new AssetGuidObjectFieldElement(property, true)
            {
                label = preferredLabel,
                objectType = Attribute.AssetType
            };
        }
    }
}
