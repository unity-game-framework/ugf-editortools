using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets
{
    [CustomPropertyDrawer(typeof(AssetIdAttribute))]
    internal class AssetIdAttributePropertyDrawer : PropertyDrawerTyped<AssetIdAttribute>
    {
        public AssetIdAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            guid = AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, label, Attribute.AssetType);

            if (scope.Changed)
            {
                GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
