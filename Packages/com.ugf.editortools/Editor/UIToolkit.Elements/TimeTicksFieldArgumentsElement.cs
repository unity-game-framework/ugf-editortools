using System;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class TimeTicksFieldArgumentsElement : BaseField<long>
    {
        public VisualElement InputElement { get; }
        public LongField TicksElement { get; }
        public SliderInt YearElement { get; }
        public SliderInt MonthElement { get; }
        public SliderInt DayElement { get; }
        public SliderInt HourElement { get; }
        public SliderInt MinuteElement { get; }
        public SliderInt SecondElement { get; }
        public SliderInt MillisecondElement { get; }

        public TimeTicksFieldArgumentsElement() : base(null, null)
        {
            InputElement = this.Query(className: inputUssClassName);

            TicksElement = new LongField("Value");

            YearElement = new SliderInt("Year", 1, 9999)
            {
                showInputField = true
            };

            MonthElement = new SliderInt("Month", 1, 12)
            {
                showInputField = true
            };

            DayElement = new SliderInt("Day", 1, 31)
            {
                showInputField = true
            };

            HourElement = new SliderInt("Hour", 0, 23)
            {
                showInputField = true
            };

            MinuteElement = new SliderInt("Minute", 0, 59)
            {
                showInputField = true
            };

            SecondElement = new SliderInt("Second", 0, 59)
            {
                showInputField = true
            };

            MillisecondElement = new SliderInt("Millisecond", 0, 999)
            {
                showInputField = true
            };

            InputElement.Add(TicksElement);
            InputElement.Add(YearElement);
            InputElement.Add(MonthElement);
            InputElement.Add(DayElement);
            InputElement.Add(HourElement);
            InputElement.Add(MinuteElement);
            InputElement.Add(SecondElement);
            InputElement.Add(MillisecondElement);

            TicksElement.RegisterValueChangedCallback(OnChange);
            YearElement.RegisterValueChangedCallback(OnChange);
            MonthElement.RegisterValueChangedCallback(OnChange);
            DayElement.RegisterValueChangedCallback(OnChange);
            HourElement.RegisterValueChangedCallback(OnChange);
            MinuteElement.RegisterValueChangedCallback(OnChange);
            SecondElement.RegisterValueChangedCallback(OnChange);
            MillisecondElement.RegisterValueChangedCallback(OnChange);
        }

        public override void SetValueWithoutNotify(long newValue)
        {
            base.SetValueWithoutNotify(newValue);

            var date = new DateTime(newValue);

            TicksElement.value = date.Ticks;
            YearElement.value = date.Year;
            MonthElement.value = date.Month;
            DayElement.highValue = DateTime.DaysInMonth(date.Year, date.Month);
            DayElement.value = date.Day;
            HourElement.value = date.Hour;
            MinuteElement.value = date.Minute;
            SecondElement.value = date.Second;
            MillisecondElement.value = date.Millisecond;
        }

        private void OnChange(ChangeEvent<long> changeEvent)
        {
            value = TicksElement.value;
        }

        private void OnChange(ChangeEvent<int> changeEvent)
        {
            DayElement.highValue = DateTime.DaysInMonth(YearElement.value, MonthElement.value);

            var date = new DateTime(
                YearElement.value,
                MonthElement.value,
                DayElement.value,
                HourElement.value,
                MinuteElement.value,
                SecondElement.value,
                MillisecondElement.value
            );

            value = date.Ticks;
        }
    }
}
