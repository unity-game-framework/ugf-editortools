using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(AssetPathAttribute), true)]
    internal class AssetPathAttributeDrawer : PropertyDrawerTyped<AssetPathAttribute>
    {
        public AssetPathAttributeDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawAssetPathField(position, serializedProperty, label, Attribute.AssetType);
        }
    }
}
