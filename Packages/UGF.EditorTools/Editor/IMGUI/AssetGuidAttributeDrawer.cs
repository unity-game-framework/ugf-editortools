using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    [CustomPropertyDrawer(typeof(AssetGuidAttribute), true)]
    internal class AssetGuidAttributeDrawer : PropertyDrawerTyped<AssetGuidAttribute>
    {
        public AssetGuidAttributeDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorIMGUIUtility.DrawAssetGuidField(position, property, label, Attribute.AssetType);
        }
    }
}
