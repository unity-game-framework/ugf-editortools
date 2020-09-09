using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorIMGUIUtility
    {
        private static readonly FieldInfo m_lastControlID;

        static EditorIMGUIUtility()
        {
            m_lastControlID = typeof(EditorGUIUtility).GetField("s_LastControlID", BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static int GetLastControlId()
        {
            return (int)m_lastControlID.GetValue(null);
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

        public static void DrawAssetGuidField(Rect position, SerializedProperty serializedProperty, GUIContent label, Type assetType)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (serializedProperty.propertyType != SerializedPropertyType.String) throw new ArgumentException("Serialized property type must be 'String'.");

            serializedProperty.stringValue = DrawAssetGuidField(position, serializedProperty.stringValue, label, assetType);
        }

        public static string DrawAssetGuidField(Rect position, string guid, GUIContent label, Type assetType)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));
            if (assetType == typeof(Scene)) assetType = typeof(SceneAsset);

            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            asset = EditorGUI.ObjectField(position, label, asset, assetType, false);

            path = AssetDatabase.GetAssetPath(asset);
            guid = AssetDatabase.AssetPathToGUID(path);

            return guid;
        }
    }
}
