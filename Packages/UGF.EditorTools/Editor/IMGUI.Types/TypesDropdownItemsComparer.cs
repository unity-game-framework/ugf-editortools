using System;
using System.Collections.Generic;
using System.Linq;
using UGF.EditorTools.Editor.IMGUI.Dropdown;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdownItemsComparer : IComparer<DropdownItem<Type>>
    {
        public static TypesDropdownItemsComparer Default { get; } = new TypesDropdownItemsComparer();

        public int Compare(DropdownItem<Type> x, DropdownItem<Type> y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            string xName = !string.IsNullOrEmpty(x.Value.FullName) ? x.Value.FullName : x.Value.Name;
            string yName = !string.IsNullOrEmpty(y.Value.FullName) ? y.Value.FullName : y.Value.Name;
            int order = yName.Count(c => c == '.') - xName.Count(c => c == '.');

            if (order == 0)
            {
                order = string.Compare(xName, yName, StringComparison.Ordinal);
            }

            return order;
        }
    }
}
