using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    internal class TimeTicksAttributePopupWindowContent : DropdownWindowContent<long>
    {
        private readonly LongField m_ticksElement;
        private readonly SliderInt m_yearElement;
        private readonly SliderInt m_monthElement;
        private readonly SliderInt m_dayElement;
        private readonly SliderInt m_hourElement;
        private readonly SliderInt m_minuteElement;
        private readonly SliderInt m_secondElement;
        private readonly SliderInt m_millisecondElement;

        public TimeTicksAttributePopupWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<long> closeHandler, long argument) : base(dropdownPosition, closeHandler, argument)
        {
            m_ticksElement = new LongField("Value");

            m_yearElement = new SliderInt("Year", 1, 9999)
            {
                showInputField = true
            };

            m_monthElement = new SliderInt("Month", 1, 12)
            {
                showInputField = true
            };

            m_dayElement = new SliderInt("Day", 1, 31)
            {
                showInputField = true
            };

            m_hourElement = new SliderInt("Hour", 0, 23)
            {
                showInputField = true
            };

            m_minuteElement = new SliderInt("Minute", 0, 59)
            {
                showInputField = true
            };

            m_secondElement = new SliderInt("Second", 0, 59)
            {
                showInputField = true
            };

            m_millisecondElement = new SliderInt("Millisecond", 0, 999)
            {
                showInputField = true
            };

            m_ticksElement.RegisterValueChangedCallback(OnChange);
            m_yearElement.RegisterValueChangedCallback(OnChange);
            m_monthElement.RegisterValueChangedCallback(OnChange);
            m_dayElement.RegisterValueChangedCallback(OnChange);
            m_hourElement.RegisterValueChangedCallback(OnChange);
            m_minuteElement.RegisterValueChangedCallback(OnChange);
            m_secondElement.RegisterValueChangedCallback(OnChange);
            m_millisecondElement.RegisterValueChangedCallback(OnChange);
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

            OnUpdate();
        }

        public override void OnCreateContent(VisualElement root)
        {
            base.OnCreateContent(root);

            root.Add(m_ticksElement);
            root.Add(m_yearElement);
            root.Add(m_monthElement);
            root.Add(m_dayElement);
            root.Add(m_hourElement);
            root.Add(m_minuteElement);
            root.Add(m_secondElement);
            root.Add(m_millisecondElement);
        }

        private void OnUpdate()
        {
            var date = new DateTime(Argument);

            m_ticksElement.value = date.Ticks;
            m_yearElement.value = date.Year;
            m_monthElement.value = date.Month;
            m_dayElement.highValue = DateTime.DaysInMonth(date.Year, date.Month);
            m_dayElement.value = date.Day;
            m_hourElement.value = date.Hour;
            m_minuteElement.value = date.Minute;
            m_secondElement.value = date.Second;
            m_millisecondElement.value = date.Millisecond;
        }

        private void OnApply()
        {
            m_dayElement.highValue = DateTime.DaysInMonth(m_yearElement.value, m_monthElement.value);

            var date = new DateTime(
                m_yearElement.value,
                m_monthElement.value,
                m_dayElement.value,
                m_hourElement.value,
                m_minuteElement.value,
                m_secondElement.value,
                m_millisecondElement.value
            );

            Argument = date.Ticks;
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            Argument = m_ticksElement.value;

            OnUpdate();
        }

        private void OnChange(ChangeEvent<int> changeEvent)
        {
            OnApply();
            OnUpdate();
        }
    }
}
