using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.UIToolkit.Elements;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownFieldElement<TValue, TItem> : DropdownButtonFieldElement<TValue> where TItem : DropdownItem
    {
        public DropdownSelection<TItem> Selection { get; } = new DropdownSelection<TItem>();
        public string ContentValueNoneLabel { get; set; } = "None";

        public DropdownFieldElement()
        {
            ButtonElement.clicked += OnDropdownButtonClicked;
            Selection.Dropdown.Selected += OnSelected;
        }

        public override void SetValueWithoutNotify(TValue newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (!showMixedValue)
            {
                OnUpdateValueContent();
            }
        }

        protected virtual void OnSelected(TItem item)
        {
        }

        protected virtual void OnUpdateValueContent()
        {
            ButtonElement.text = ContentValueNoneLabel;
        }

        protected virtual IEnumerable<TItem> OnGetItems()
        {
            return ArraySegment<TItem>.Empty;
        }

        private void OnDropdownButtonClicked()
        {
            Selection.Show(ButtonElement.worldBound, 0, OnGetItems());
        }
    }
}
