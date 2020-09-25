using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static partial class EditorIMGUIUtility
    {
        [Obsolete("DrawAssetGuidField has been deprecated. Use AttributeEditorGUIUtility.DrawAssetGuidField instead.")]
        public static void DrawAssetGuidField(Rect position, SerializedProperty serializedProperty, GUIContent label, Type assetType)
        {
            AttributeEditorGUIUtility.DrawAssetGuidField(position, serializedProperty, label, assetType);
        }

        [Obsolete("DrawAssetGuidField has been deprecated. Use AttributeEditorGUIUtility.DrawAssetGuidField instead.")]
        public static string DrawAssetGuidField(Rect position, string guid, GUIContent label, Type assetType)
        {
            return AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, label, assetType);
        }
    }
}
