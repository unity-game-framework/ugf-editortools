using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    internal class TimeSpanTicksAttributePopupWindowContent : DropdownWindowContent<long>
    {
        private readonly TimeSpanTicksFieldArgumentsElement m_argumentsElement = new TimeSpanTicksFieldArgumentsElement();

        public TimeSpanTicksAttributePopupWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<long> closeHandler, long argument) : base(dropdownPosition, closeHandler, argument)
        {
            m_argumentsElement.RegisterCallback<ChangeEvent<long>>(OnChange);
        }

        public override void OnOpen()
        {
            base.OnOpen();

            m_argumentsElement.value = Argument;
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

            root.Add(m_argumentsElement);
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            Argument = m_argumentsElement.value;
        }
    }
}
