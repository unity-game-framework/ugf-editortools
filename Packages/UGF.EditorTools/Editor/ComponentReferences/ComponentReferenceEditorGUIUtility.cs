﻿using System;
using UGF.EditorTools.Editor.FileIds;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.ComponentReferences
{
    public static class ComponentReferenceEditorGUIUtility
    {
        public static void DrawReferenceField(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyFileId = serializedProperty.FindPropertyRelative("m_fileId");
            SerializedProperty propertyComponent = serializedProperty.FindPropertyRelative("m_component");

            Object previous = propertyComponent.objectReferenceValue;

            EditorGUI.ObjectField(position, propertyComponent, label);

            Object component = propertyComponent.objectReferenceValue;

            if (component != null)
            {
                if (string.IsNullOrEmpty(propertyFileId.stringValue) || previous != component)
                {
                    propertyFileId.stringValue = FileIdEditorUtility.GetFileId(component).ToString();
                }
            }
            else
            {
                propertyFileId.stringValue = string.Empty;
            }
        }
    }
}