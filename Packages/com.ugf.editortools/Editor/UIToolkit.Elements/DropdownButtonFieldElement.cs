using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class DropdownButtonFieldElement<TValue> : BaseField<TValue>
    {
        public DropdownButtonElement ButtonElement { get; }

        public DropdownButtonFieldElement() : base(null, new DropdownButtonElement())
        {
            ButtonElement = this.Query<DropdownButtonElement>(className: inputUssClassName).First();
            ButtonElement.AddToClassList(BasePopupField<bool, bool>.inputUssClassName);
            ButtonElement.AddToClassList(PopupField<bool>.inputUssClassName);
            ButtonElement.RemoveFromClassList(Button.ussClassName);
            ButtonElement.RemoveFromClassList(Button.iconUssClassName);
            ButtonElement.RemoveFromClassList(TextElement.ussClassName);

            AddToClassList(BasePopupField<bool, bool>.ussClassName);
            AddToClassList(PopupField<bool>.ussClassName);
        }

        protected override void UpdateMixedValueContent()
        {
            if (showMixedValue)
            {
                ButtonElement.text = mixedValueString;
            }
        }
    }
}
