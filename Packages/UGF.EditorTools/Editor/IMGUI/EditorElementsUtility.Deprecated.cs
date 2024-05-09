using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static partial class EditorElementsUtility
    {
        [Obsolete("TimeTicksField has been deprecated, use overload with dropdown implementation instead.")]
        public static long TimeTicksField(GUIContent label, long value, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TimeTicksField(position, label, value);
        }

        [Obsolete("TimeTicksField has been deprecated, use overload with dropdown implementation instead.")]
        public static long TimeTicksField(Rect position, GUIContent label, long value)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            m_styles ??= new Styles();

            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectSign = new Rect(rectField.x, rectField.y + 1F, rectField.height, rectField.height);
            var rectFields = new Rect(rectSign.xMax + space, rectField.y, rectField.width - rectField.height - space, rectField.height);

            bool sign = value >= 0;

            if (!sign)
            {
                value = -value;
            }

            var date = new DateTime(value);

            int[] segments =
            {
                date.Year,
                date.Month,
                date.Day,
                date.Hour,
                date.Minute,
                date.Second,
                (int)(date.Ticks - new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second).Ticks)
            };

            if (GUI.Button(rectSign, sign ? m_styles.SignPlusContent : m_styles.SignMinusContent, EditorStyles.iconButton))
            {
                sign = !sign;
            }

            EditorGUI.MultiIntField(rectFields, m_styles.DateSegmentsContent, segments);

            segments[0] = Mathf.Clamp(segments[0], 1, 9999);
            segments[1] = Mathf.Clamp(segments[1], 1, 12);
            segments[2] = Mathf.Clamp(segments[2], 1, DateTime.DaysInMonth(segments[0], segments[1]));
            segments[3] = Mathf.Clamp(segments[3], 0, 23);
            segments[4] = Mathf.Clamp(segments[4], 0, 59);
            segments[5] = Mathf.Clamp(segments[5], 0, 59);
            segments[6] = Mathf.Clamp(segments[6], 0, 9999999);

            date = new DateTime(segments[0], segments[1], segments[2], segments[3], segments[4], segments[5]);
            date = date.AddTicks(segments[6]);
            value = date.Ticks;

            if (!sign)
            {
                value = -value;
            }

            return value;
        }

        [Obsolete("TimeSpanTicksField has been deprecated, use overload with dropdown implementation instead.")]
        public static long TimeSpanTicksField(GUIContent label, long value, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TimeSpanTicksField(position, label, value);
        }

        [Obsolete("TimeSpanTicksField has been deprecated, use overload with dropdown implementation instead.")]
        public static long TimeSpanTicksField(Rect position, GUIContent label, long value)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            m_styles ??= new Styles();

            float space = EditorGUIUtility.standardVerticalSpacing;

            Rect rectField = EditorGUI.PrefixLabel(position, label);
            var rectSign = new Rect(rectField.x, rectField.y + 1F, rectField.height, rectField.height);
            var rectFields = new Rect(rectSign.xMax + space, rectField.y, rectField.width - rectField.height - space, rectField.height);

            bool sign = value >= 0;

            if (!sign)
            {
                value = -value;
            }

            var span = new TimeSpan(value);

            int[] segments =
            {
                span.Days,
                span.Hours,
                span.Minutes,
                span.Seconds,
                span.Milliseconds,
                (int)(span.Ticks - new TimeSpan(span.Days, span.Hours, span.Minutes, span.Seconds, span.Milliseconds).Ticks)
            };

            if (GUI.Button(rectSign, sign ? m_styles.SignPlusContent : m_styles.SignMinusContent, EditorStyles.iconButton))
            {
                sign = !sign;
            }

            EditorGUI.MultiIntField(rectFields, m_styles.SpanSegmentsContent, segments);

            span = new TimeSpan(segments[0], segments[1], segments[2], segments[3], segments[4]);
            span += new TimeSpan(segments[5]);
            value = span.Ticks;

            if (!sign)
            {
                value = -value;
            }

            return value;
        }
    }
}
