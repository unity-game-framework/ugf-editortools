using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetIdEditorUtility
    {
        public static GlobalId GetId(Object asset)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            string guid = AssetsEditorUtility.GetGuid(asset);

            return new GlobalId(guid);
        }

        public static bool CheckAssetIdAttributeType(SerializedProperty serializedProperty, Object asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            FieldInfo field = SerializedPropertyEditorUtility.GetFieldInfoAndType(serializedProperty, out _);
            var attribute = field.GetCustomAttribute<AssetIdAttribute>();

            return attribute != null && attribute.AssetType.IsInstanceOfType(asset);
        }

        public static void SetAssetToAssetIdReference(SerializedProperty serializedProperty, Object asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            GlobalIdEditorUtility.SetAssetToProperty(propertyGuid, asset);

            propertyAsset.objectReferenceValue = asset;
        }

        public static bool TryGetAssetIdReferenceTypeFromCollection(SerializedProperty serializedProperty, out Type type)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            Type fieldType = SerializedPropertyEditorUtility.GetFieldType(serializedProperty);
            Type assetIdReferenceType = null;

            if (fieldType.IsArray)
            {
                assetIdReferenceType = fieldType.GetElementType();
            }
            else if (fieldType.IsGenericType)
            {
                Type definition = fieldType.GetGenericTypeDefinition();

                if (definition == typeof(List<>))
                {
                    assetIdReferenceType = fieldType.GetGenericArguments()[0];
                }
            }

            if (assetIdReferenceType != null)
            {
                type = assetIdReferenceType.GetGenericArguments()[0];
                return true;
            }

            type = default;
            return false;
        }
    }
}
