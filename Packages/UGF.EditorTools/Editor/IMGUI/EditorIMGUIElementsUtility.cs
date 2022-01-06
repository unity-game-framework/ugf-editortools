using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorIMGUIElementsUtility
    {
        public static void DrawPairedField(SerializedProperty propertyFirst, SerializedProperty propertySecond)
        {
            Rect position = EditorGUILayout.GetControlRect();

            // position = EditorGUI.IndentedRect(position);

            DrawPairedField(position, propertyFirst, propertySecond);
        }

        public static void DrawPairedField(Rect position, SerializedProperty propertyFirst, SerializedProperty propertySecond)
        {
            DrawPairedField(position, new GUIContent(propertyFirst.displayName), new GUIContent(propertySecond.displayName), propertyFirst, propertySecond);
        }

        public static void DrawPairedField(Rect position, GUIContent labelFirst, GUIContent labelSecond, SerializedProperty propertyFirst, SerializedProperty propertySecond)
        {
            if (labelFirst == null) throw new ArgumentNullException(nameof(labelFirst));
            if (labelSecond == null) throw new ArgumentNullException(nameof(labelSecond));
            if (propertyFirst == null) throw new ArgumentNullException(nameof(propertyFirst));
            if (propertySecond == null) throw new ArgumentNullException(nameof(propertySecond));

            GetPairedFieldPositions(position, out Rect rectFirst, out Rect rectSecond);

            using (new LabelWidthMinScope(labelFirst))
            {
                EditorGUI.PropertyField(rectFirst, propertyFirst, labelFirst);
            }

            using (new LabelWidthMinScope(labelSecond))
            {
                EditorGUI.PropertyField(rectSecond, propertySecond, labelSecond);
            }
        }

        public static void GetPairedFieldPositions(Rect position, out Rect first, out Rect second)
        {
            float indent = EditorIMGUIUtility.GetIndent();
            var labelPosition = new Rect(position.x + indent, position.y, EditorGUIUtility.labelWidth - indent, position.height);
            var rect = new Rect(position.x + EditorGUIUtility.labelWidth + 2F, position.y, position.width - EditorGUIUtility.labelWidth - 2F, position.height);

            float space = EditorGUIUtility.standardVerticalSpacing * 2F;
            float labelWidth = EditorGUIUtility.labelWidth + EditorIMGUIUtility.IndentPerLevel;

            first = new Rect(position.x, position.y, labelWidth, position.height);
            second = new Rect(first.xMax + space, position.y, position.width - first.width - space, position.height);

            first = labelPosition;
            second = rect;
        }

        public static float GetLabelWidth(GUIContent content)
        {
            return GetLabelWidth(content, EditorStyles.label);
        }

        public static float GetLabelWidth(GUIContent content, GUIStyle style)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (style == null) throw new ArgumentNullException(nameof(style));

            style.CalcMinMaxWidth(content, out float min, out float _);

            return min;
        }
    }
}
