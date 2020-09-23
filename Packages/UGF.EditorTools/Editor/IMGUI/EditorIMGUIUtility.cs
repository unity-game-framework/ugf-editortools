using System;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorIMGUIUtility
    {
        private static readonly FieldInfo m_lastControlID;
        private static readonly PropertyInfo m_indent;

        static EditorIMGUIUtility()
        {
            m_lastControlID = typeof(EditorGUIUtility).GetField("s_LastControlID", BindingFlags.NonPublic | BindingFlags.Static);
            m_indent = typeof(EditorGUI).GetProperty("indent", BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static int GetLastControlId()
        {
            return (int)m_lastControlID.GetValue(null);
        }

        public static float GetIndent()
        {
            return (float)m_indent.GetMethod.Invoke(null, Array.Empty<object>());
        }

        public static bool DrawDefaultInspector(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            EditorGUI.BeginChangeCheck();

            serializedObject.UpdateIfRequiredOrScript();

            SerializedProperty iterator = serializedObject.GetIterator();

            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope(iterator.propertyPath == "m_Script"))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }

            serializedObject.ApplyModifiedProperties();

            return EditorGUI.EndChangeCheck();
        }

        public static void DrawSerializedPropertyChildren(SerializedObject serializedObject, string propertyName)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyName));

            SerializedProperty serializedProperty = serializedObject.FindProperty(propertyName);

            if (serializedProperty == null)
            {
                throw new ArgumentException($"The property by the specified name not found: '{propertyName}'.");
            }

            SerializedProperty propertyEnd = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            if (!SerializedProperty.EqualContents(serializedProperty, propertyEnd))
            {
                EditorGUILayout.PropertyField(serializedProperty, true);

                while (serializedProperty.NextVisible(false) && !SerializedProperty.EqualContents(serializedProperty, propertyEnd))
                {
                    EditorGUILayout.PropertyField(serializedProperty, true);
                }
            }
        }

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
