using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    internal class TimeSpanTicksAttributePopupWindowContent : DropdownWindowContent<long>
    {
        private readonly LongField m_ticksElement;
        private readonly SliderInt m_daysElement;
        private readonly SliderInt m_hoursElement;
        private readonly SliderInt m_minutesElement;
        private readonly SliderInt m_secondsElement;
        private readonly SliderInt m_millisecondsElement;

        public TimeSpanTicksAttributePopupWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<long> closeHandler, long argument) : base(dropdownPosition, closeHandler, argument)
        {
            m_ticksElement = new LongField("Value");

            m_daysElement = new SliderInt("Days", TimeSpan.MinValue.Days + 1, TimeSpan.MaxValue.Days - 1)
            {
                showInputField = true
            };

            m_hoursElement = new SliderInt("Hours", -23, 23)
            {
                showInputField = true
            };

            m_minutesElement = new SliderInt("Minutes", -59, 59)
            {
                showInputField = true
            };

            m_secondsElement = new SliderInt("Seconds", -59, 59)
            {
                showInputField = true
            };

            m_millisecondsElement = new SliderInt("Milliseconds", -999, 999)
            {
                showInputField = true
            };

            m_ticksElement.RegisterValueChangedCallback(OnChange);
            m_daysElement.RegisterValueChangedCallback(OnChange);
            m_hoursElement.RegisterValueChangedCallback(OnChange);
            m_minutesElement.RegisterValueChangedCallback(OnChange);
            m_secondsElement.RegisterValueChangedCallback(OnChange);
            m_millisecondsElement.RegisterValueChangedCallback(OnChange);
        }

        public override void OnOpen()
        {
            base.OnOpen();

            OnUpdate();
        }

        protected override void OnGUILayout()
        {
            Argument = EditorGUILayout.LongField("Value", Argument);
            Argument = Math.Clamp(Argument, TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks);

            var span = new TimeSpan(Argument);

            int days = EditorGUILayout.IntSlider("Days", span.Days, TimeSpan.MinValue.Days + 1, TimeSpan.MaxValue.Days - 1);
            int hours = EditorGUILayout.IntSlider("Hours", span.Hours, -23, 23);
            int minutes = EditorGUILayout.IntSlider("Minutes", span.Minutes, -59, 59);
            int seconds = EditorGUILayout.IntSlider("Seconds", span.Seconds, -59, 59);
            int milliseconds = EditorGUILayout.IntSlider("Milliseconds", span.Milliseconds, -999, 999);

            span = new TimeSpan(days, hours, minutes, seconds, milliseconds);

            Argument = span.Ticks;
        }

        public override void OnCreateContent(VisualElement root)
        {
            base.OnCreateContent(root);

            root.Add(m_ticksElement);
            root.Add(m_daysElement);
            root.Add(m_hoursElement);
            root.Add(m_minutesElement);
            root.Add(m_secondsElement);
            root.Add(m_millisecondsElement);
        }

        private void OnUpdate()
        {
            var span = new TimeSpan(Argument);

            m_ticksElement.value = span.Ticks;
            m_daysElement.value = span.Days;
            m_hoursElement.value = span.Hours;
            m_minutesElement.value = span.Minutes;
            m_secondsElement.value = span.Seconds;
            m_millisecondsElement.value = span.Milliseconds;
        }

        private void OnApply()
        {
            var span = new TimeSpan(m_daysElement.value, m_hoursElement.value, m_minutesElement.value, m_secondsElement.value, m_millisecondsElement.value);

            Argument = span.Ticks;
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            OnApply();
            OnUpdate();
        }

        private void OnChange(ChangeEvent<int> changeEvent)
        {
            OnApply();
            OnUpdate();
        }
    }
}
