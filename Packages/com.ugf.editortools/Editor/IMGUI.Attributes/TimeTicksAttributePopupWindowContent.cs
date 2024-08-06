using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    internal class TimeTicksAttributePopupWindowContent : DropdownWindowContent<long>
    {
        private readonly TimeTicksFieldArgumentsElement m_argumentsElement = new TimeTicksFieldArgumentsElement();

        public TimeTicksAttributePopupWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<long> closeHandler, long argument) : base(dropdownPosition, closeHandler, argument)
        {
            m_argumentsElement.RegisterCallback<ChangeEvent<long>>(OnChange);
        }

        protected override void OnGUILayout()
        {
            Argument = EditorGUILayout.LongField("Value", Argument);
            Argument = Math.Clamp(Argument, DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);

            var date = new DateTime(Argument);

            int year = EditorGUILayout.IntSlider("Year", date.Year, 1, 9999);
            int month = EditorGUILayout.IntSlider("Month", date.Month, 1, 12);
            int day = EditorGUILayout.IntSlider("Day", date.Day, 1, DateTime.DaysInMonth(date.Year, date.Month));
            int hour = EditorGUILayout.IntSlider("Hour", date.Hour, 0, 23);
            int minute = EditorGUILayout.IntSlider("Minute", date.Minute, 0, 59);
            int second = EditorGUILayout.IntSlider("Second", date.Second, 0, 59);
            int millisecond = EditorGUILayout.IntSlider("Millisecond", date.Millisecond, 0, 999);

            day = Mathf.Clamp(day, 1, DateTime.DaysInMonth(year, month));

            date = new DateTime(year, month, day, hour, minute, second, millisecond);

            Argument = date.Ticks;
        }

        public override void OnOpen()
        {
            base.OnOpen();

            m_argumentsElement.value = Argument;
        }

        public override void OnCreateContent(VisualElement root)
        {
            base.OnCreateContent(root);

            root.Add(m_argumentsElement);
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            Argument = m_argumentsElement.value;
        }
    }
}
