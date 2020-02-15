using UGF.EditorTools.Runtime.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    [CustomPropertyDrawer(typeof(AssetTypeAttribute), true)]
    internal class AssetTypeAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                var assetTypeAttribute = (AssetTypeAttribute)attribute;

                EditorGUI.ObjectField(position, property, assetTypeAttribute.AssetType, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);

                Debug.LogWarning($"Serialized property type must be 'ObjectReference' in order to use 'AssetTypeAttribute'. Property path: '{property.propertyPath}'.");
            }

            EditorGUI.EndProperty();
        }
    }
}
