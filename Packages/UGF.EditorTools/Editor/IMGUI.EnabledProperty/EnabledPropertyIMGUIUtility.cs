using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.EnabledProperty
{
    public static class EnabledPropertyGUIUtility
    {
        public static bool EnabledProperty(GUIContent label, SerializedProperty serializedProperty, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return EnabledProperty(position, label, serializedProperty);
        }

        public static bool EnabledProperty(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");
            bool hasVisibleChildren = HasVisibleChildren(propertyValue);

            return EnabledProperty(position, label, serializedProperty, hasVisibleChildren);
        }

        public static bool EnabledProperty(Rect position, GUIContent label, SerializedProperty serializedProperty, bool hasVisibleChildren)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyEnabled = serializedProperty.FindPropertyRelative("m_enabled");
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            float line = EditorGUIUtility.singleLineHeight;
            float valuePadding = line;
            int valueIndent = hasVisibleChildren ? 1 : 0;

            position = EditorGUI.IndentedRect(position);

            var enabledPosition = new Rect(position.x, position.y, line, line);
            var valuePosition = new Rect(position.x, position.y, position.width, position.height);

            valuePosition.xMin += valuePadding;

            propertyEnabled.boolValue = GUI.Toggle(enabledPosition, propertyEnabled.boolValue, GUIContent.none);

            using (new LabelWidthChangeScope(-valuePadding, true))
            using (new IndentLevelScope(valueIndent))
            using (new EditorGUI.DisabledScope(!propertyEnabled.boolValue))
            {
                EditorGUI.PropertyField(valuePosition, propertyValue, label, true);
            }

            return propertyEnabled.boolValue;
        }

        private static bool HasVisibleChildren(SerializedProperty serializedProperty)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.Rect:
                case SerializedPropertyType.Bounds:
                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                case SerializedPropertyType.RectInt:
                case SerializedPropertyType.BoundsInt: return false;
                default:
                    return serializedProperty.hasVisibleChildren;
            }
        }
    }
}
