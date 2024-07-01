namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public delegate void DropdownItemHandler<in TItem>(TItem item) where TItem : DropdownItem;
}
