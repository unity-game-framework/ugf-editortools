using System;
using UGF.EditorTools.Editor.Assets;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    public static class AttributeEditorGUIUtility
    {
        internal static GUIContent ContentFolderIcon { get { return m_contentFolderIcon ??= new GUIContent(EditorGUIUtility.FindTexture("FolderOpened Icon")); } }

        private static GUIContent m_contentFolderIcon;

        public static string DrawSelectFileField(GUIContent label, string path, string title, string directory, string extension, bool inAssets = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawSelectFileField(position, label, path, title, directory, extension, inAssets);
        }

        public static string DrawSelectFileField(Rect position, GUIContent label, string path, string title, string directory, string extension, bool inAssets = true)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (path == null) throw new ArgumentNullException(nameof(path));

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectText = new Rect(rectField.x, rectField.y, rectField.width - height - space, rectField.height);
            var rectButton = new Rect(rectText.xMax + space, rectField.y, height, rectField.height);

            path = GUI.TextField(rectText, path);

            if (GUI.Button(rectButton, ContentFolderIcon, EditorStyles.iconButton))
            {
                path = AssetsEditorUtility.OpenFileSelection(title, directory, extension, inAssets);
            }

            return path;
        }

        public static string DrawSelectDirectoryField(GUIContent label, string path, string title, string directory, bool inAssets = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawSelectDirectoryField(position, label, path, title, directory, inAssets);
        }

        public static string DrawSelectDirectoryField(Rect position, GUIContent label, string path, string title, string directory, bool inAssets = true)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (path == null) throw new ArgumentNullException(nameof(path));

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectText = new Rect(rectField.x, rectField.y, rectField.width - height - space, rectField.height);
            var rectButton = new Rect(rectText.xMax + space, rectField.y, height, rectField.height);

            path = GUI.TextField(rectText, path);

            if (GUI.Button(rectButton, ContentFolderIcon, EditorStyles.iconButton))
            {
                path = AssetsEditorUtility.OpenDirectorySelection(title, directory, inAssets);
            }

            return path;
        }

        public static void DrawAssetGuidField(SerializedProperty serializedProperty, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawAssetGuidField(position, serializedProperty, label, assetType);
        }

        public static void DrawAssetGuidField(Rect position, SerializedProperty serializedProperty, GUIContent label, Type assetType)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (serializedProperty.propertyType != SerializedPropertyType.String) throw new ArgumentException("Serialized property type must be 'String'.");

            serializedProperty.stringValue = DrawAssetGuidField(position, serializedProperty.stringValue, label, assetType);
        }

        public static string DrawAssetGuidField(string guid, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawAssetGuidField(position, guid, label, assetType);
        }

        public static string DrawAssetGuidField(Rect position, string guid, GUIContent label, Type assetType)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            path = DrawAssetPathField(position, path, label, assetType);

            guid = AssetDatabase.AssetPathToGUID(path);

            return guid;
        }

        public static void DrawAssetPathField(SerializedProperty serializedProperty, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawAssetPathField(position, serializedProperty, label, assetType);
        }

        public static void DrawAssetPathField(Rect position, SerializedProperty serializedProperty, GUIContent label, Type assetType)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (serializedProperty.propertyType != SerializedPropertyType.String) throw new ArgumentException("Serialized property type must be 'String'.");

            serializedProperty.stringValue = DrawAssetPathField(position, serializedProperty.stringValue, label, assetType);
        }

        public static string DrawAssetPathField(string path, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawAssetPathField(position, path, label, assetType);
        }

        public static string DrawAssetPathField(Rect position, string path, GUIContent label, Type assetType)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (assetType == typeof(Scene)) assetType = typeof(SceneAsset);

            Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (!string.IsNullOrEmpty(path) && asset == null)
            {
                asset = EditorIMGUIUtility.MissingObject;
            }

            asset = EditorGUI.ObjectField(position, label, asset, assetType, false);

            if (!EditorIMGUIUtility.IsMissingObject(asset))
            {
                path = AssetDatabase.GetAssetPath(asset);
            }

            return path;
        }

        public static void DrawResourcesPathField(SerializedProperty serializedProperty, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawResourcesPathField(position, serializedProperty, label, assetType);
        }

        public static void DrawResourcesPathField(Rect position, SerializedProperty serializedProperty, GUIContent label, Type assetType)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (serializedProperty.propertyType != SerializedPropertyType.String) throw new ArgumentException("Serialized property type must be 'String'.");

            serializedProperty.stringValue = DrawResourcesPathField(position, serializedProperty.stringValue, label, assetType);
        }

        public static string DrawResourcesPathField(string path, GUIContent label, Type assetType, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawResourcesPathField(position, path, label, assetType);
        }

        public static string DrawResourcesPathField(Rect position, string path, GUIContent label, Type assetType)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (assetType == typeof(Scene)) assetType = typeof(SceneAsset);

            Object asset = Resources.Load(path, assetType);

            if (!string.IsNullOrEmpty(path) && asset == null)
            {
                asset = EditorIMGUIUtility.MissingObject;
            }

            asset = EditorGUI.ObjectField(position, label, asset, assetType, false);

            if (!EditorIMGUIUtility.IsMissingObject(asset))
            {
                path = asset != null && AssetsEditorUtility.TryGetResourcesPath(asset, out string result) ? result : string.Empty;
            }

            return path;
        }
    }
}
