﻿namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownFieldElement<TValue> : DropdownFieldElement<TValue, DropdownItem<TValue>>
    {
        protected override void OnSelected(DropdownItem<TValue> item)
        {
            base.OnSelected(item);

            value = item.Value;
        }

        protected override void OnUpdateValueContent()
        {
            ButtonElement.text = value?.ToString() ?? ContentValueNoneLabel;
        }
    }
}
