using UGF.EditorTools.Runtime.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    [CustomPropertyDrawer(typeof(AssetGuidAttribute), true)]
    internal class AssetGuidAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                var assetGuidAttribute = (AssetGuidAttribute)attribute;

                EditorIMGUIUtility.DrawAssetGuidField(position, property, label, assetGuidAttribute.AssetType);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);

                Debug.LogWarning($"Serialized property type must be 'String' in order to use 'AssetGuidAttribute'. Property path: '{property.propertyPath}'.");
            }

            EditorGUI.EndProperty();
        }
    }
}
