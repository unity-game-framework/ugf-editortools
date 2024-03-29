﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Serialized
{
    public static class SerializedPropertyEditorUtility
    {
        private static readonly GetFieldInfoFromPropertyHandler m_getFieldInfoFromProperty;
        private static readonly ValidateObjectFieldAssignmentHandler m_validateObjectFieldAssignmentMethod;

        private delegate FieldInfo GetFieldInfoFromPropertyHandler(SerializedProperty serializedProperty, out Type type);
        private delegate Object ValidateObjectFieldAssignmentHandler(Object[] references, Type objectType, SerializedProperty serializedProperty, int options);

        static SerializedPropertyEditorUtility()
        {
            var type = Type.GetType("UnityEditor.ScriptAttributeUtility, UnityEditor.CoreModule", true);

            MethodInfo getFieldInfoFromPropertyMethod = type.GetMethod("GetFieldInfoFromProperty", BindingFlags.NonPublic | BindingFlags.Static)
                                                        ?? throw new ArgumentException("Method not found by the specified name: 'GetFieldInfoFromProperty'.");

            m_getFieldInfoFromProperty = (GetFieldInfoFromPropertyHandler)getFieldInfoFromPropertyMethod.CreateDelegate(typeof(GetFieldInfoFromPropertyHandler));

            MethodInfo validateObjectFieldAssignmentMethod = typeof(EditorGUI).GetMethod("ValidateObjectFieldAssignment", BindingFlags.NonPublic | BindingFlags.Static)
                                                             ?? throw new ArgumentException($"Method not found by the specified name: 'ValidateObjectFieldAssignment'.");

            m_validateObjectFieldAssignmentMethod = (ValidateObjectFieldAssignmentHandler)validateObjectFieldAssignmentMethod.CreateDelegate(typeof(ValidateObjectFieldAssignmentHandler));
        }

        public static Type GetFieldType(SerializedProperty serializedProperty)
        {
            FieldInfo fieldInfo = GetFieldInfoAndType(serializedProperty, out _);

            return fieldInfo.FieldType;
        }

        public static FieldInfo GetFieldInfoAndType(SerializedProperty serializedProperty, out Type type)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            return m_getFieldInfoFromProperty.Invoke(serializedProperty, out type);
        }

        public static bool TryValidateObjectFieldAssignment(SerializedProperty serializedProperty, Object target, out Object result)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));
            if (target == null) throw new ArgumentNullException(nameof(target));

            result = m_validateObjectFieldAssignmentMethod.Invoke(new[] { target }, null, serializedProperty, 0);

            return result != null;
        }

        public static IEnumerable<SerializedProperty> GetChildrenVisible(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            SerializedProperty iterator = serializedObject.GetIterator();

            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                yield return iterator;
            }
        }

        public static IEnumerable<SerializedProperty> GetChildrenVisible(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyEnd = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            if (!SerializedProperty.EqualContents(serializedProperty, propertyEnd))
            {
                yield return serializedProperty;

                while (serializedProperty.NextVisible(false) && !SerializedProperty.EqualContents(serializedProperty, propertyEnd))
                {
                    yield return serializedProperty;
                }
            }
        }

        public static IEnumerable<SerializedProperty> GetChildren(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            SerializedProperty iterator = serializedObject.GetIterator();

            for (bool enterChildren = true; iterator.Next(enterChildren); enterChildren = false)
            {
                yield return iterator;
            }
        }

        public static IEnumerable<SerializedProperty> GetChildren(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyEnd = serializedProperty.GetEndProperty();

            serializedProperty.Next(true);

            if (!SerializedProperty.EqualContents(serializedProperty, propertyEnd))
            {
                yield return serializedProperty;

                while (serializedProperty.Next(false) && !SerializedProperty.EqualContents(serializedProperty, propertyEnd))
                {
                    yield return serializedProperty;
                }
            }
        }
    }
}
