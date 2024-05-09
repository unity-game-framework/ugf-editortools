using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    internal class TimeSpanTicksAttributePopupWindowContent : DropdownWindowContent<long>
    {
        public TimeSpanTicksAttributePopupWindowContent(Rect dropdownPosition, DropdownWindowContentArgumentHandler<long> closeHandler, long argument) : base(dropdownPosition, closeHandler, argument)
        {
        }

        protected override void OnGUILayout()
        {
            Argument = EditorGUILayout.LongField("Value", Argument);
            Argument = Math.Clamp(Argument, TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks);

            var span = new TimeSpan(Argument);

            int day = EditorGUILayout.IntSlider("Days", span.Days, TimeSpan.MinValue.Days, TimeSpan.MaxValue.Days);
            int hour = EditorGUILayout.IntSlider("Hours", span.Hours, -23, 23);
            int minute = EditorGUILayout.IntSlider("Minutes", span.Minutes, -59, 59);
            int second = EditorGUILayout.IntSlider("Seconds", span.Seconds, -59, 59);
            int milliseconds = EditorGUILayout.IntSlider("Milliseconds", span.Milliseconds, -999, 999);

            span = new TimeSpan(day, hour, minute, second, milliseconds);

            Argument = span.Ticks;
        }
    }
}
