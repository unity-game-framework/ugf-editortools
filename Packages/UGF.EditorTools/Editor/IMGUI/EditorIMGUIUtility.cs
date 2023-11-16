using System;
using System.Reflection;
using UGF.EditorTools.Editor.Serialized;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorIMGUIUtility
    {
        public static float IndentPerLevel { get; }
        public static Object MissingObject { get; }

        private static readonly FieldInfo m_lastControlID;
        private static readonly PropertyInfo m_indent;
        private const string PROPERTY_SCRIPT_NAME = "m_Script";

        static EditorIMGUIUtility()
        {
            MissingObject = ScriptableObject.CreateInstance<ScriptableObject>();
            MissingObject.hideFlags = HideFlags.HideAndDontSave;

            Object.DestroyImmediate(MissingObject);

            m_lastControlID = typeof(EditorGUIUtility).GetField("s_LastControlID", BindingFlags.NonPublic | BindingFlags.Static)
                              ?? throw new ArgumentException("Field not found by the specified name: 's_LastControlID'.");

            m_indent = typeof(EditorGUI).GetProperty("indent", BindingFlags.NonPublic | BindingFlags.Static)
                       ?? throw new ArgumentException("Property not found by the specified name: 'indent'.");

            FieldInfo kIndentPerLevel = typeof(EditorGUI).GetField("kIndentPerLevel", BindingFlags.NonPublic | BindingFlags.Static)
                                        ?? throw new ArgumentException("Field not found by the specified name: 'kIndentPerLevel'.");

            IndentPerLevel = (float)kIndentPerLevel.GetValue(null);
        }

        public static bool IsMissingObject(Object target)
        {
            return ReferenceEquals(target, MissingObject);
        }

        public static int GetLastControlId()
        {
            return (int)m_lastControlID.GetValue(null);
        }

        public static float GetIndent()
        {
            return (float)m_indent.GetMethod.Invoke(null, Array.Empty<object>());
        }

        public static float GetIndentWithLevel(int level)
        {
            return IndentPerLevel * level;
        }

        public static bool IsControlHasMainActionEvent(int controlId, Event controlEvent)
        {
            return GUIUtility.keyboardControl == controlId
                   && controlEvent.type == EventType.KeyDown
                   && controlEvent.keyCode is KeyCode.Space or KeyCode.Return or KeyCode.KeypadEnter
                   && !(controlEvent.alt || controlEvent.shift || controlEvent.command || controlEvent.control);
        }

        public static SerializedProperty GetScriptProperty(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            SerializedProperty propertyScript = serializedObject.FindProperty(PROPERTY_SCRIPT_NAME);

            return propertyScript;
        }

        public static void DrawScriptProperty(SerializedObject serializedObject)
        {
            SerializedProperty propertyScript = GetScriptProperty(serializedObject);

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(propertyScript);
            }
        }

        public static void DrawPropertyChildrenVisible(Rect position, SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            position.height = EditorGUIUtility.singleLineHeight;

            foreach (SerializedProperty property in SerializedPropertyEditorUtility.GetChildrenVisible(serializedProperty))
            {
                EditorGUI.PropertyField(position, property);

                position.y += EditorGUI.GetPropertyHeight(property) + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        public static void DrawPropertyChildrenVisible(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            foreach (SerializedProperty property in SerializedPropertyEditorUtility.GetChildrenVisible(serializedProperty))
            {
                EditorGUILayout.PropertyField(property);
            }
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

        public static float GetHeightPropertyChildrenVisible(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            float height = 0F;
            int count = serializedProperty.Copy().CountInProperty() - 1;

            foreach (SerializedProperty property in SerializedPropertyEditorUtility.GetChildrenVisible(serializedProperty))
            {
                height += EditorGUI.GetPropertyHeight(property);

                if (--count > 0)
                {
                    height += EditorGUIUtility.standardVerticalSpacing;
                }
            }

            return height;
        }
    }
}
