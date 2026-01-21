using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown.Advanced;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class Dropdown<TItem> : AdvancedDropdown where TItem : DropdownItem
    {
        public List<TItem> Items { get; }
        public string RootName { get; set; } = "Root";
        public float MinimumWidth { get { return minimumSize.x; } set { minimumSize = new Vector2(value, minimumSize.y); } }
        public float MinimumHeight { get { return minimumSize.y; } set { minimumSize = new Vector2(minimumSize.x, value); } }
        public IComparer<AdvancedDropdownItem> ItemComparer { get; set; }

        public event DropdownItemHandler<TItem> Selected;

        public Dropdown(IEnumerable<TItem> items = null, AdvancedDropdownState state = null) : base(state)
        {
            Items = items != null ? new List<TItem>(items) : new List<TItem>();
            ItemComparer = new AdvancedDropdownItemComparer(Items);
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

                AdvancedDropdownItem dropdownItem = OnItemCreate(item);

                if (item.HasPath)
                {
                    DropdownEditorUtility.AddChild(root, dropdownItem, item.Path);
                }
                else
                {
                    root.AddChild(dropdownItem);
                }

                dropdownItem.id = i;
            }

            DropdownEditorUtility.SortChildren(root, ItemComparer);

            return root;
        }

        protected virtual AdvancedDropdownItem OnItemCreate(TItem item)
        {
            var dropdownItem = new AdvancedDropdownItem(item.Name)
            {
                enabled = item.Enabled
            };

            if (item.HasIcon)
            {
                dropdownItem.icon = item.Icon;
            }

            return dropdownItem;
        }
    }
}
