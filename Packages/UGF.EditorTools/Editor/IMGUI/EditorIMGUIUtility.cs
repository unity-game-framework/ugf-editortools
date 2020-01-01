using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorIMGUIUtility
    {
        public static bool DrawDefaultInspector(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            EditorGUI.BeginChangeCheck();

            serializedObject.UpdateIfRequiredOrScript();

            SerializedProperty iterator = serializedObject.GetIterator();

            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                {
                    EditorGUILayout.PropertyField(iterator, true, null);
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
                    EditorGUILayout.PropertyField(serializedProperty, true, null);
                }
            }
        }
    }
}
