using System;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class TimeSpanTicksFieldArgumentsElement : BaseField<long>
    {
        public VisualElement InputElement { get; }
        public LongField TicksElement { get; }
        public SliderInt DaysElement { get; }
        public SliderInt HoursElement { get; }
        public SliderInt MinutesElement { get; }
        public SliderInt SecondsElement { get; }
        public SliderInt MillisecondsElement { get; }

        public TimeSpanTicksFieldArgumentsElement() : base(null, null)
        {
            InputElement = this.Query(className: inputUssClassName);

            TicksElement = new LongField("Value");

            DaysElement = new SliderInt("Days", TimeSpan.MinValue.Days + 1, TimeSpan.MaxValue.Days - 1)
            {
                showInputField = true
            };

            HoursElement = new SliderInt("Hours", -23, 23)
            {
                showInputField = true
            };

            MinutesElement = new SliderInt("Minutes", -59, 59)
            {
                showInputField = true
            };

            SecondsElement = new SliderInt("Seconds", -59, 59)
            {
                showInputField = true
            };

            MillisecondsElement = new SliderInt("Milliseconds", -999, 999)
            {
                showInputField = true
            };

            InputElement.Add(TicksElement);
            InputElement.Add(DaysElement);
            InputElement.Add(HoursElement);
            InputElement.Add(MinutesElement);
            InputElement.Add(SecondsElement);
            InputElement.Add(MillisecondsElement);

            TicksElement.RegisterValueChangedCallback(OnChange);
            DaysElement.RegisterValueChangedCallback(OnChange);
            HoursElement.RegisterValueChangedCallback(OnChange);
            MinutesElement.RegisterValueChangedCallback(OnChange);
            SecondsElement.RegisterValueChangedCallback(OnChange);
            MillisecondsElement.RegisterValueChangedCallback(OnChange);
        }

        public override void SetValueWithoutNotify(long newValue)
        {
            base.SetValueWithoutNotify(newValue);

            var span = new TimeSpan(newValue);

            TicksElement.value = span.Ticks;
            DaysElement.value = span.Days;
            HoursElement.value = span.Hours;
            MinutesElement.value = span.Minutes;
            SecondsElement.value = span.Seconds;
            MillisecondsElement.value = span.Milliseconds;
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            value = TicksElement.value;
        }

        private void OnChange(ChangeEvent<int> changeEvent)
        {
            var span = new TimeSpan(DaysElement.value, HoursElement.value, MinutesElement.value, SecondsElement.value, MillisecondsElement.value);

            value = span.Ticks;
        }
    }
}
