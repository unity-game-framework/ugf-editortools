using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class Dropdown<TItem> : AdvancedDropdown where TItem : DropdownItem
    {
        public string RootName { get; set; } = "Root";
        public List<TItem> Items { get; set; } = new List<TItem>();

        public event Action<TItem> Selected;

        public Dropdown(AdvancedDropdownState state = null) : base(state ?? new AdvancedDropdownState())
        {
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item.id < 0 || item.id >= Items.Count)
            {
                throw new ArgumentException($"Item not found with specified id: '{item.id}', name:'{item.name}'.");
            }

            TItem selected = Items[item.id];

            Selected?.Invoke(selected);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem(RootName);

            for (int i = 0; i < Items.Count; i++)
            {
                TItem item = Items[i];

                var dropdownItem = new AdvancedDropdownItem(item.Name)
                {
                    id = i,
                    enabled = item.Enabled
                };

                if (item.HasIcon)
                {
                    dropdownItem.icon = item.Icon;
                }

                if (item.HasPath)
                {
                    DropdownUtility.AddMenuItem(root, dropdownItem, item.Path);
                }
                else
                {
                    root.AddChild(dropdownItem);
                }
            }

            return root;
        }
    }
}
