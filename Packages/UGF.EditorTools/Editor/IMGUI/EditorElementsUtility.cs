using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static partial class EditorElementsUtility
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

            m_styles ??= new Styles();

            var rectField = new Rect(position.x, position.y, position.width - position.height, position.height);
            var rectButton = new Rect(rectField.xMax, position.y, position.height, position.height);

            value = EditorGUI.TextField(rectField, label, value, m_styles.TextFieldDropdownFieldStyle);

            if (DropdownEditorGUIUtility.Dropdown(rectButton, GUIContent.none, GUIContent.none, m_selection, itemsHandler, out DropdownItem<string> selected, FocusType.Keyboard, m_styles.TextFieldDropdownButtonStyle))
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

            TimeTicksField(position, label, serializedProperty.longValue, serializedProperty, arguments =>
            {
                try
                {
                    arguments.Arguments.longValue = arguments.Value;
                    arguments.Arguments.serializedObject.ApplyModifiedProperties();
                }
                catch
                {
                    // ignored
                }
            });
        }

        public static void TimeTicksField(Rect position, GUIContent label, long value, DropdownWindowContentArgumentHandler<long> resultHandler)
        {
            TimeTicksField(position, label, value, resultHandler, arguments => arguments.Arguments.Invoke(arguments.Value));
        }

        public static void TimeTicksField<TArguments>(Rect position, GUIContent label, long value, TArguments arguments, DropdownWindowContentArgumentHandler<(TArguments Arguments, long Value)> resultHandler)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (resultHandler == null) throw new ArgumentNullException(nameof(resultHandler));

            value = Math.Clamp(value, DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);

            var date = new DateTime(value);
            string valueText = value.ToString();

            using var iconScope = new AssetFieldIconReferenceScope(position, "Ticks", valueText);

            var content = new GUIContent(date.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            if (DropdownEditorGUIUtility.DropdownButton(position, label, content, out Rect dropdownPosition))
            {
                PopupWindow.Show(dropdownPosition, new TimeTicksAttributePopupWindowContent(dropdownPosition, resultValue => { resultHandler.Invoke((arguments, resultValue)); }, value));
            }

            if (iconScope.Clicked)
            {
                EditorGUIUtility.systemCopyBuffer = valueText;
            }
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

            TimeSpanTicksField(position, label, serializedProperty.longValue, serializedProperty, arguments =>
            {
                arguments.Arguments.longValue = arguments.Value;
                arguments.Arguments.serializedObject.ApplyModifiedProperties();
            });
        }

        public static void TimeSpanTicksField(Rect position, GUIContent label, long value, DropdownWindowContentArgumentHandler<long> resultHandler)
        {
            TimeSpanTicksField(position, label, value, resultHandler, arguments => arguments.Arguments.Invoke(arguments.Value));
        }

        public static void TimeSpanTicksField<TArguments>(Rect position, GUIContent label, long value, TArguments arguments, DropdownWindowContentArgumentHandler<(TArguments Arguments, long Value)> resultHandler)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (resultHandler == null) throw new ArgumentNullException(nameof(resultHandler));

            value = Math.Clamp(value, DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);

            var span = new TimeSpan(value);
            string valueText = value.ToString();

            using var iconScope = new AssetFieldIconReferenceScope(position, "Ticks", valueText);

            var content = new GUIContent(span.ToString("G"));

            if (DropdownEditorGUIUtility.DropdownButton(position, label, content, out Rect dropdownPosition))
            {
                PopupWindow.Show(dropdownPosition, new TimeSpanTicksAttributePopupWindowContent(dropdownPosition, resultValue => { resultHandler.Invoke((arguments, resultValue)); }, value));
            }

            if (iconScope.Clicked)
            {
                EditorGUIUtility.systemCopyBuffer = valueText;
            }
        }
    }
}
