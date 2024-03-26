using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorElementsUtility
    {
        private static readonly DropdownSelection<DropdownItem<string>> m_selection = new DropdownSelection<DropdownItem<string>>();
        private static Styles m_styles;

        private class Styles
        {
            public GUIStyle TextFieldDropdownButtonStyle { get; } = new GUIStyle("TextFieldDropDown");
            public GUIStyle TextFieldDropdownFieldStyle { get; } = new GUIStyle("TextFieldDropDownText");
            public GUIContent SignPlusContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Plus"), "Sign is plus.");
            public GUIContent SignMinusContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Toolbar Minus"), "Sign is minus.");

            public GUIContent[] DateSegmentsContent { get; } = new[]
            {
                new GUIContent("Y", "Year of the date."),
                new GUIContent("M", "Month of the year."),
                new GUIContent("D", "Day of the month."),
                new GUIContent("H", "Hour of the day."),
                new GUIContent("M", "Minute of the hour."),
                new GUIContent("S", "Second of the minute."),
                new GUIContent("T", "Tick of the second.")
            };

            public GUIContent[] SpanSegmentsContent { get; } = new[]
            {
                new GUIContent("D", "Days."),
                new GUIContent("H", "Hours"),
                new GUIContent("M", "Minutes."),
                new GUIContent("S", "Seconds."),
                new GUIContent("M", "Milliseconds."),
                new GUIContent("T", "Ticks.")
            };
        }

        static EditorElementsUtility()
        {
            m_selection.Dropdown.RootName = "Select";
        }

        public static void TextFieldWithDropdown(SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            GUIContent label = EditorGUIUtility.TrTempContent(serializedProperty.displayName);

            TextFieldWithDropdown(label, serializedProperty, itemsHandler, options);
        }

        public static void TextFieldWithDropdown(GUIContent label, SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            TextFieldWithDropdown(position, label, serializedProperty, itemsHandler);
        }

        public static void TextFieldWithDropdown(Rect position, GUIContent label, SerializedProperty serializedProperty, Func<IEnumerable<DropdownItem<string>>> itemsHandler)
        {
            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            string value = TextFieldWithDropdown(position, label, serializedProperty.stringValue, itemsHandler);

            if (scope.Changed)
            {
                serializedProperty.stringValue = value;
            }
        }

        public static string TextFieldWithDropdown(string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            return TextFieldWithDropdown(GUIContent.none, value, itemsHandler, options);
        }

        public static string TextFieldWithDropdown(GUIContent label, string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TextFieldWithDropdown(position, label, value, itemsHandler);
        }

        public static string TextFieldWithDropdown(Rect position, GUIContent label, string value, Func<IEnumerable<DropdownItem<string>>> itemsHandler)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (itemsHandler == null) throw new ArgumentNullException(nameof(itemsHandler));

            Styles styles = GetStyles();

            var rectField = new Rect(position.x, position.y, position.width - position.height, position.height);
            var rectButton = new Rect(rectField.xMax, position.y, position.height, position.height);

            value = EditorGUI.TextField(rectField, label, value, styles.TextFieldDropdownFieldStyle);

            if (DropdownEditorGUIUtility.Dropdown(rectButton, GUIContent.none, GUIContent.none, m_selection, itemsHandler, out DropdownItem<string> selected, FocusType.Keyboard, styles.TextFieldDropdownButtonStyle))
            {
                value = selected.Value;
            }

            return value;
        }

        public static void TimeTicksField(SerializedProperty serializedProperty, params GUILayoutOption[] options)
        {
            GUIContent label = EditorGUIUtility.TrTempContent(serializedProperty.displayName);

            TimeTicksField(label, serializedProperty, options);
        }

        public static void TimeTicksField(GUIContent label, SerializedProperty serializedProperty, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            TimeTicksField(position, label, serializedProperty);
        }

        public static void TimeTicksField(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            long value = TimeTicksField(position, label, serializedProperty.longValue);

            if (scope.Changed)
            {
                serializedProperty.longValue = value;
            }
        }

        public static long TimeTicksField(GUIContent label, long value, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TimeTicksField(position, label, value);
        }

        public static long TimeTicksField(Rect position, GUIContent label, long value)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Styles styles = GetStyles();
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

            if (GUI.Button(rectSign, sign ? styles.SignPlusContent : styles.SignMinusContent, EditorStyles.iconButton))
            {
                sign = !sign;
            }

            EditorGUI.MultiIntField(rectFields, styles.DateSegmentsContent, segments);

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

        public static void TimeSpanTicksField(SerializedProperty serializedProperty, params GUILayoutOption[] options)
        {
            GUIContent label = EditorGUIUtility.TrTempContent(serializedProperty.displayName);

            TimeSpanTicksField(label, serializedProperty, options);
        }

        public static void TimeSpanTicksField(GUIContent label, SerializedProperty serializedProperty, params GUILayoutOption[] options)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            TimeSpanTicksField(position, label, serializedProperty);
        }

        public static void TimeSpanTicksField(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            long value = TimeSpanTicksField(position, label, serializedProperty.longValue);

            if (scope.Changed)
            {
                serializedProperty.longValue = value;
            }
        }

        public static long TimeSpanTicksField(GUIContent label, long value, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none, options);

            return TimeSpanTicksField(position, label, value);
        }

        public static long TimeSpanTicksField(Rect position, GUIContent label, long value)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Styles styles = GetStyles();
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

            if (GUI.Button(rectSign, sign ? styles.SignPlusContent : styles.SignMinusContent, EditorStyles.iconButton))
            {
                sign = !sign;
            }

            EditorGUI.MultiIntField(rectFields, styles.SpanSegmentsContent, segments);

            span = new TimeSpan(segments[0], segments[1], segments[2], segments[3], segments[4]);
            span += new TimeSpan(segments[5]);
            value = span.Ticks;

            if (!sign)
            {
                value = -value;
            }

            return value;
        }

        private static Styles GetStyles()
        {
            return m_styles ??= new Styles();
        }
    }
}
