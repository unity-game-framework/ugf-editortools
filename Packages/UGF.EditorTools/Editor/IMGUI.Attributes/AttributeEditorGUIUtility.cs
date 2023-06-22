using System;
using System.IO;
using System.Reflection;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.FileIds;
using UGF.EditorTools.Editor.Serialized;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    public static class AttributeEditorGUIUtility
    {
        internal static GUIContent ContentFolderIcon { get { return m_contentFolderIcon ??= new GUIContent(EditorGUIUtility.FindTexture("FolderOpened Icon")); } }

        private static readonly FileId m_fileIdContent;
        private static GUIContent m_contentFolderIcon;

        static AttributeEditorGUIUtility()
        {
            m_fileIdContent = ScriptableObject.CreateInstance<FileId>();
            m_fileIdContent.hideFlags = HideFlags.HideAndDontSave;
        }

        public static bool CheckAssetIdAttributeType(SerializedProperty serializedProperty, Object asset)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            FieldInfo field = SerializedPropertyEditorUtility.GetFieldInfoAndType(serializedProperty, out _);
            var attribute = field.GetCustomAttribute<AssetIdAttribute>();

            return attribute != null && attribute.AssetType.IsInstanceOfType(asset);
        }

        public static void DrawFileIdField(GUIContent label, SerializedProperty serializedProperty, Type assetType, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawFileIdField(position, label, serializedProperty, assetType);
        }

        public static void DrawFileIdField(Rect position, GUIContent label, SerializedProperty serializedProperty, Type assetType)
        {
            serializedProperty.stringValue = DrawFileIdField(position, label, serializedProperty.stringValue, assetType);
        }

        public static string DrawFileIdField(GUIContent label, string value, Type assetType, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawFileIdField(position, label, value, assetType);
        }

        public static string DrawFileIdField(Rect position, GUIContent label, string value, Type assetType)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (assetType == typeof(Scene)) assetType = typeof(SceneAsset);

            try
            {
                Object content = null;

                if (!string.IsNullOrEmpty(value))
                {
                    content = m_fileIdContent;
                    content.name = ObjectNames.NicifyVariableName(assetType.Name);
                }

                Object selected = EditorGUI.ObjectField(position, label, content, assetType, true);

                if (selected != content)
                {
                    value = selected != null ? FileIdEditorUtility.GetFileId(selected).ToString() : string.Empty;
                }

                return value;
            }
            finally
            {
                m_fileIdContent.name = "File Id";
            }
        }

        public static void DrawSelectFileField(SerializedProperty serializedProperty, GUIContent label, string title, string defaultDirectory, string extension, bool relative = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawSelectFileField(position, serializedProperty, label, title, defaultDirectory, extension, relative);
        }

        public static void DrawSelectFileField(Rect position, SerializedProperty serializedProperty, GUIContent label, string title, string defaultDirectory, string extension, bool relative = true)
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
                if (!string.IsNullOrEmpty(serializedProperty.stringValue))
                {
                    string fileDirectory = Path.GetDirectoryName(serializedProperty.stringValue);

                    if (!string.IsNullOrEmpty(fileDirectory))
                    {
                        defaultDirectory = fileDirectory;
                    }
                }

                if (AssetsEditorUtility.TrySelectFile(title, defaultDirectory, extension, relative, out string path))
                {
                    serializedProperty.stringValue = path;
                    serializedProperty.serializedObject.ApplyModifiedProperties();
                }

                GUIUtility.ExitGUI();
            }
        }

        public static void DrawSelectDirectoryField(SerializedProperty serializedProperty, GUIContent label, string title, string defaultDirectory, bool relative = true, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawSelectDirectoryField(position, serializedProperty, label, title, defaultDirectory, relative);
        }

        public static void DrawSelectDirectoryField(Rect position, SerializedProperty serializedProperty, GUIContent label, string title, string defaultDirectory, bool relative = true)
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
                if (!string.IsNullOrEmpty(serializedProperty.stringValue))
                {
                    defaultDirectory = serializedProperty.stringValue;
                }

                if (AssetsEditorUtility.TrySelectDirectory(title, defaultDirectory, relative, out string path))
                {
                    serializedProperty.stringValue = path;
                    serializedProperty.serializedObject.ApplyModifiedProperties();
                }

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

            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

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
