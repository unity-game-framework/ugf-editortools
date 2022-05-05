using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListKeyAndValueDrawer : ReorderableListDrawer
    {
        public string PropertyKeyName { get; }
        public string PropertyValueName { get; }

        public ReorderableListKeyAndValueDrawer(SerializedProperty serializedProperty, string propertyKeyName = "m_key", string propertyValueName = "m_value") : base(serializedProperty)
        {
            if (string.IsNullOrEmpty(propertyKeyName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyKeyName));
            if (string.IsNullOrEmpty(propertyValueName)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyValueName));

            PropertyKeyName = propertyKeyName;
            PropertyValueName = propertyValueName;
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative(PropertyKeyName);
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative(PropertyValueName);

            float space = EditorGUIUtility.standardVerticalSpacing;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            var rectKey = new Rect(position.x, position.y, labelWidth, position.height);
            var rectValue = new Rect(rectKey.xMax + space, position.y, position.width - rectKey.width - space, position.height);

            OnDrawKey(rectKey, propertyKey);
            OnDrawValue(rectValue, propertyValue);
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }

        protected virtual void OnDrawKey(Rect position, SerializedProperty serializedProperty)
        {
            EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
        }

        protected virtual void OnDrawValue(Rect position, SerializedProperty serializedProperty)
        {
            EditorGUI.PropertyField(position, serializedProperty, GUIContent.none);
        }
    }
}
