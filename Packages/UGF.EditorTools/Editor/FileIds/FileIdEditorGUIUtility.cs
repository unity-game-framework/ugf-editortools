using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.FileIds
{
    public static class FileIdEditorGUIUtility
    {
        private static readonly FileId m_fileIdContent;

        static FileIdEditorGUIUtility()
        {
            m_fileIdContent = ScriptableObject.CreateInstance<FileId>();
            m_fileIdContent.hideFlags = HideFlags.HideAndDontSave;
        }

        public static void DrawFileIdField(GUIContent label, SerializedProperty serializedProperty, Type assetType, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            DrawFileIdField(position, label, serializedProperty, assetType);
        }

        public static void DrawFileIdField(Rect position, GUIContent label, SerializedProperty serializedProperty, Type assetType)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");
            
            propertyValue.longValue = (long)DrawFileIdField(position, label, (ulong)propertyValue.longValue, assetType);
        }

        public static ulong DrawFileIdField(GUIContent label, ulong value, Type assetType, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return DrawFileIdField(position, label, value, assetType);
        }

        public static ulong DrawFileIdField(Rect position, GUIContent label, ulong value, Type assetType)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (assetType == typeof(Scene)) assetType = typeof(SceneAsset);

            try
            {
                Object content = null;

                if (value > 0U)
                {
                    content = m_fileIdContent;
                    content.name = ObjectNames.NicifyVariableName(assetType.Name);
                }

                Object selected = EditorGUI.ObjectField(position, label, content, assetType, true);

                if (selected != content)
                {
                    value = selected != null ? FileIdEditorUtility.GetFileId(selected) : 0U;
                }

                return value;
            }
            finally
            {
                m_fileIdContent.name = "File Id";
            }
        }
    }
}
