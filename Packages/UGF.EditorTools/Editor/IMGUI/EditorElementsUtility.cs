using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public static class EditorElementsUtility
    {
        private static readonly DropdownSelection<DropdownItem<string>> m_selection = new DropdownSelection<DropdownItem<string>>();
        private static readonly int m_timeTicksFieldHint = nameof(TimeTicksField).GetHashCode();
        private static readonly int m_timeSpanTicksFieldHint = nameof(TimeTicksField).GetHashCode();
        private static int? m_timeTicksFieldLastControlId;
        private static long? m_timeTicksFieldLastValue;
        private static int? m_timeSpanTicksFieldLastControlId;
        private static long? m_timeSpanTicksFieldLastValue;
        private static Styles m_styles;

        private class Styles
        {
            public GUIStyle TextFieldDropdownButtonStyle { get; } = new GUIStyle("TextFieldDropDown");
            public GUIStyle TextFieldDropdownFieldStyle { get; } = new GUIStyle("TextFieldDropDownText");
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

            int controlId = GUIUtility.GetControlID(m_timeTicksFieldHint, FocusType.Keyboard, position);

            TimeTicksField(position, label, value, controlId, argument =>
            {
                m_timeTicksFieldLastControlId = argument.Arguments;
                m_timeTicksFieldLastValue = argument.Value;
            });

            if (m_timeTicksFieldLastControlId == controlId && m_timeTicksFieldLastValue.HasValue)
            {
                value = m_timeTicksFieldLastValue.Value;

                m_timeTicksFieldLastControlId = default;
                m_timeTicksFieldLastValue = default;

                GUI.changed = true;
            }

            return value;
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

            string valueText = new DateTime(value).ToString("yyyy-MM-dd HH:mm:ss.fff");

            using var iconScope = new AssetFieldIconReferenceScope(position, "Date", valueText);

            if (DropdownEditorGUIUtility.DropdownButton(position, label, new GUIContent(valueText), out Rect dropdownPosition))
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

            int controlId = GUIUtility.GetControlID(m_timeSpanTicksFieldHint, FocusType.Keyboard, position);

            TimeSpanTicksField(position, label, value, controlId, argument =>
            {
                m_timeSpanTicksFieldLastControlId = argument.Arguments;
                m_timeSpanTicksFieldLastValue = argument.Value;
            });

            if (m_timeSpanTicksFieldLastControlId == controlId && m_timeSpanTicksFieldLastValue.HasValue)
            {
                value = m_timeSpanTicksFieldLastValue.Value;

                m_timeSpanTicksFieldLastControlId = default;
                m_timeSpanTicksFieldLastValue = default;

                GUI.changed = true;
            }

            return value;
        }

        public static void TimeSpanTicksField(Rect position, GUIContent label, long value, DropdownWindowContentArgumentHandler<long> resultHandler)
        {
            TimeSpanTicksField(position, label, value, resultHandler, arguments => arguments.Arguments.Invoke(arguments.Value));
        }

        public static void TimeSpanTicksField<TArguments>(Rect position, GUIContent label, long value, TArguments arguments, DropdownWindowContentArgumentHandler<(TArguments Arguments, long Value)> resultHandler)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (resultHandler == null) throw new ArgumentNullException(nameof(resultHandler));

            value = Math.Clamp(value, TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks);

            string valueText = $@"{(value < 0 ? "-" : "")}{new TimeSpan(value):d\:hh\:mm\:ss\.fff}";

            using var iconScope = new AssetFieldIconReferenceScope(position, "Time", valueText);

            if (DropdownEditorGUIUtility.DropdownButton(position, label, new GUIContent(valueText), out Rect dropdownPosition))
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
