using System;
using System.Reflection;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetIdEditorUtility
    {
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
    }
}
