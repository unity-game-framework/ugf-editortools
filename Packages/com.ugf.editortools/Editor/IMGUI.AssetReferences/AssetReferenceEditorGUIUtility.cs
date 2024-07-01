using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.AssetReferences
{
    public static class AssetReferenceEditorGUIUtility
    {
        public static void AssetReference(Rect position, GUIContent label, SerializedProperty serializedProperty)
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

                propertyGuid.stringValue = guid;
            }
        }
    }
}
