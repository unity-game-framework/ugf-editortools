using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown.Advanced
{
    public class AdvancedDropdownItemComparer : IComparer<AdvancedDropdownItem>
    {
        public IReadOnlyList<DropdownItem> Items { get; }

        public AdvancedDropdownItemComparer(IReadOnlyList<DropdownItem> items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public int Compare(AdvancedDropdownItem x, AdvancedDropdownItem y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            int xPriority = GetPriority(x);
            int yPriority = GetPriority(y);
            int compare = yPriority.CompareTo(xPriority);

            if (compare == 0)
            {
                bool xHasChildren = HasChildren(x);
                bool yHasChildren = HasChildren(y);

                compare = yHasChildren.CompareTo(xHasChildren);

                return compare == 0 ? string.Compare(x.name, y.name, StringComparison.Ordinal) : compare;
            }

            return compare;
        }

        private bool HasChildren(AdvancedDropdownItem item)
        {
            var children = (List<AdvancedDropdownItem>)item.children;

            return children.Count > 0;
        }

        private int GetPriority(AdvancedDropdownItem item)
        {
            int id = item.id;

            if (id >= 0 && id < Items.Count)
            {
                DropdownItem dropdownItem = Items[id];

                return dropdownItem.Priority;
            }

            return 0;
        }
    }
}
