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

                Rect rect = EditorGUI.PrefixLabel(position, label);

                string guid = property.stringValue;
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Object asset = AssetDatabase.LoadAssetAtPath(path, assetGuidAttribute.AssetType);

                asset = EditorGUI.ObjectField(rect, asset, assetGuidAttribute.AssetType, false);

                path = AssetDatabase.GetAssetPath(asset);
                guid = AssetDatabase.AssetPathToGUID(path);

                property.stringValue = guid;
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
