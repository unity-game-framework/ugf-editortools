using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    [CustomPropertyDrawer(typeof(AssetIdReferenceListAttribute), true)]
    internal class AssetIdReferenceListAttributePropertyDrawer : PropertyDrawerTyped<AssetIdReferenceListAttribute>
    {
        public AssetIdReferenceListAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            OnDrawPropertyDefault(position, serializedProperty, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new AssetIdReferenceListElement(property, true)
            {
                headerTitle = preferredLabel,
                DisplayAsReplace = Attribute.DisplayAsReplace,
                DisplayReplaceButton = Attribute.DisplayReplaceButton
            };
        }
    }
}
