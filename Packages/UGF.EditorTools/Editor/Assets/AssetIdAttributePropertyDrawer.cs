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
    internal class AssetIdAttributePropertyDrawer : PropertyDrawer<AssetIdAttribute>
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            if (serializedProperty.propertyType == SerializedPropertyType.Generic)
            {
                using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

                string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

                guid = AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, label, Attribute.AssetType);

                if (scope.Changed)
                {
                    GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
                }
            }
            else if (serializedProperty.propertyType == SerializedPropertyType.Hash128)
            {
                using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

                Hash128 hash128 = AttributeEditorGUIUtility.DrawAssetHash128Field(position, serializedProperty.hash128Value, label, Attribute.AssetType);

                if (scope.Changed)
                {
                    serializedProperty.hash128Value = hash128;
                }
            }
            else
            {
                OnDrawPropertyDefault(position, serializedProperty, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
