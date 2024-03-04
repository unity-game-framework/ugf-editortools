using System;
using System.IO;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.FileIds;
using UGF.EditorTools.Editor.IMGUI.Scopes;
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
        private static readonly int m_objectPickerFieldControlIdHint = nameof(m_objectPickerFieldControlIdHint).GetHashCode();
        private static GUIContent m_contentFolderIcon;
        private static Rect? m_assetFieldIconPosition;
        private static string m_assetFieldIconTooltip;
        private static Styles m_styles;

        private class Styles
        {
            public GUIStyle FieldIconButton { get; } = new GUIStyle(EditorStyles.iconButton);
            public GUIContent FieldIconContent { get; } = EditorGUIUtility.IconContent("UnityEditor.FindDependencies");
        }

        static AttributeEditorGUIUtility()
        {
            m_fileIdContent = ScriptableObject.CreateInstance<FileId>();
            m_fileIdContent.hideFlags = HideFlags.HideAndDontSave;
        }

        public static void DrawObjectPickerField(GUIContent label, SerializedProperty serializedProperty, Type targetType, string filter = "")
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none);

            DrawObjectPickerField(position, label, serializedProperty, targetType, filter);
        }

        public static void DrawObjectPickerField(Rect position, GUIContent label, SerializedProperty serializedProperty, Type targetType, string filter = "")
        {
            bool allowSceneObjects = !EditorUtility.IsPersistent(serializedProperty.serializedObject.targetObject);

            serializedProperty.objectReferenceValue = DrawObjectPickerField(position, label, serializedProperty.objectReferenceValue, targetType, filter, allowSceneObjects);
        }

        public static Object DrawObjectPickerField(GUIContent label, Object target, Type targetType, string filter = "", bool allowSceneObjects = false)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none);

            return DrawObjectPickerField(position, label, target, targetType, filter, allowSceneObjects);
        }

        public static Object DrawObjectPickerField(Rect position, GUIContent label, Object target, Type targetType, string filter = "", bool allowSceneObjects = false)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            Event currentEvent = Event.current;
            int controlId = GUIUtility.GetControlID(m_objectPickerFieldControlIdHint, FocusType.Keyboard, position);
            var rectButton = new Rect(position.xMax - 19F, position.y, 19F, position.height);

            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                {
                    if (rectButton.Contains(currentEvent.mousePosition) && GUI.enabled)
                    {
                        GUIUtility.keyboardControl = controlId;

                        EditorGUIUtility.ShowObjectPicker<Object>(target, allowSceneObjects, filter, controlId);

                        currentEvent.Use();

                        GUIUtility.ExitGUI();
                    }

                    break;
                }
                case EventType.KeyDown:
                {
                    if (EditorIMGUIUtility.IsControlHasMainActionEvent(controlId, currentEvent))
                    {
                        EditorGUIUtility.ShowObjectPicker<Object>(target, allowSceneObjects, filter, controlId);

                        currentEvent.Use();

                        GUIUtility.ExitGUI();
                    }

                    break;
                }
            }

            target = EditorGUI.ObjectField(position, label, target, targetType, allowSceneObjects);

            if (currentEvent.type == EventType.ExecuteCommand
                && EditorGUIUtility.GetObjectPickerControlID() == controlId
                && currentEvent.commandName is "ObjectSelectorUpdated" or "ObjectSelectorClosed")
            {
                target = EditorGUIUtility.GetObjectPickerObject();

                currentEvent.Use();
            }

            return target;
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
                string tooltip = !string.IsNullOrEmpty(value) ? $"File Id: {value}" : "File Id: None";

                using var scope = new AssetFieldIconScope(position, tooltip);

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

                if (scope.Clicked)
                {
                    EditorGUIUtility.systemCopyBuffer = value;
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
            string tooltip = !string.IsNullOrEmpty(guid) ? $"GUID: {guid}" : "GUID: None";

            using var scope = new AssetFieldIconScope(position, tooltip);

            string path = AssetDatabase.GUIDToAssetPath(guid);
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

            guid = AssetDatabase.AssetPathToGUID(path);

            if (scope.Clicked)
            {
                EditorGUIUtility.systemCopyBuffer = guid;
            }

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

            string tooltip = !string.IsNullOrEmpty(path) ? $"Asset Path: {path}" : "Asset Path: None";

            using var scope = new AssetFieldIconScope(position, tooltip);

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

            if (scope.Clicked)
            {
                EditorGUIUtility.systemCopyBuffer = path;
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

            string tooltip = !string.IsNullOrEmpty(path) ? $"Resource Path: {path}" : "Resource Path: None";

            using var scope = new AssetFieldIconScope(position, tooltip);

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

            if (scope.Clicked)
            {
                EditorGUIUtility.systemCopyBuffer = path;
            }

            return path;
        }

        public static bool BeginAssetFieldIcon(Rect position, string tooltip)
        {
            if (string.IsNullOrEmpty(tooltip)) throw new ArgumentException("Value cannot be null or empty.", nameof(tooltip));

            if (m_assetFieldIconPosition.HasValue)
            {
                throw new InvalidOperationException("BeginAssetFieldIcon method must be paired with calling of EndAssetFieldIcon.");
            }

            m_styles ??= new Styles();

            float height = EditorGUIUtility.singleLineHeight;

            position = new Rect(position.xMax - height * 2F, position.y + 1F, height, height);

            m_assetFieldIconPosition = position;
            m_assetFieldIconTooltip = tooltip;

            return Event.current.type != EventType.Repaint && GUI.Button(position, m_styles.FieldIconContent, m_styles.FieldIconButton);
        }

        public static void EndAssetFieldIcon()
        {
            if (!m_assetFieldIconPosition.HasValue)
            {
                throw new InvalidOperationException("BeginAssetFieldIcon method must be paired with calling of EndAssetFieldIcon.");
            }

            if (Event.current.type == EventType.Repaint)
            {
                m_styles.FieldIconContent.tooltip = m_assetFieldIconTooltip;

                Rect position = m_assetFieldIconPosition.Value;

                if (position.Contains(Event.current.mousePosition))
                {
                    GUI.Button(position, m_styles.FieldIconContent, m_styles.FieldIconButton);
                }
                else
                {
                    using (new GUIContentColorScope(new Color(1F, 1F, 1F, 0.25F)))
                    {
                        GUI.Button(position, m_styles.FieldIconContent, m_styles.FieldIconButton);
                    }
                }
            }

            m_assetFieldIconPosition = default;
            m_assetFieldIconTooltip = string.Empty;
        }
    }
}
