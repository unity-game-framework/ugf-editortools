using System;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetIdReferenceEditorGUIUtility
    {
        public static void AssetIdReferenceField(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            SerializedProperty propertyGuid = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            EditorGUI.ObjectField(position, propertyAsset, label);

            if (scope.Changed)
            {
                string path = AssetDatabase.GetAssetPath(propertyAsset.objectReferenceValue);
                string guid = AssetDatabase.AssetPathToGUID(path);

                GlobalIdEditorUtility.SetGuidToProperty(propertyGuid, guid);
            }
        }
    }
}
