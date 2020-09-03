using System;
using System.Collections.Generic;
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

        public event Action<TItem> Selected;

        public Dropdown(AdvancedDropdownState state = null) : base(state ?? new AdvancedDropdownState())
        {
            Items = new List<TItem>();
        }

        public Dropdown(IEnumerable<TItem> items, AdvancedDropdownState state = null) : base(state)
        {
            Items = new List<TItem>(items);
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
                    DropdownEditorUtility.AddMenuItem(root, dropdownItem, item.Path);
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
