using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

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
    }
}
