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

        public static void DrawSelectFileField(SerializedProperty serializedProperty, GUIContent label, string title, string directory, string extension, bool inAssets = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawSelectFileField(position, serializedProperty, label, title, directory, extension, inAssets);
        }

        public static void DrawSelectFileField(Rect position, SerializedProperty serializedProperty, GUIContent label, string title, string directory, string extension, bool inAssets = true)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (label == null) throw new ArgumentNullException(nameof(label));

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectText = new Rect(rectField.x, rectField.y, rectField.width - height - space, rectField.height);
            var rectButton = new Rect(rectText.xMax + space, rectField.y, height, rectField.height);

            serializedProperty.stringValue = GUI.TextField(rectText, serializedProperty.stringValue);

            if (GUI.Button(rectButton, ContentFolderIcon, EditorStyles.iconButton))
            {
                serializedProperty.stringValue = AssetsEditorUtility.OpenFileSelection(title, directory, extension, inAssets);
                serializedProperty.serializedObject.ApplyModifiedProperties();

                GUIUtility.ExitGUI();
            }
        }

        public static void DrawSelectDirectoryField(SerializedProperty serializedProperty, GUIContent label, string title, string directory, bool inAssets = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawSelectDirectoryField(position, serializedProperty, label, title, directory, inAssets);
        }

        public static void DrawSelectDirectoryField(Rect position, SerializedProperty serializedProperty, GUIContent label, string title, string directory, bool inAssets = true)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (label == null) throw new ArgumentNullException(nameof(label));

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectText = new Rect(rectField.x, rectField.y, rectField.width - height - space, rectField.height);
            var rectButton = new Rect(rectText.xMax + space, rectField.y, height, rectField.height);

            serializedProperty.stringValue = GUI.TextField(rectText, serializedProperty.stringValue);

            if (GUI.Button(rectButton, ContentFolderIcon, EditorStyles.iconButton))
            {
                serializedProperty.stringValue = AssetsEditorUtility.OpenDirectorySelection(title, directory, inAssets);
                serializedProperty.serializedObject.ApplyModifiedProperties();

                GUIUtility.ExitGUI();
            }
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
