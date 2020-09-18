namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownItem<TValue> : DropdownItem
    {
        public TValue Value { get; set; }

        public DropdownItem(string name = "Item", TValue value = default, bool enabled = true) : base(name, enabled)
        {
            Value = value;
        }
    }
}
