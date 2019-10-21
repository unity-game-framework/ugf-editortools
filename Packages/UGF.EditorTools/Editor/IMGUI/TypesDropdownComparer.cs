using System;
using System.Collections.Generic;
using System.Linq;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class TypesDropdownComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            string xName = !string.IsNullOrEmpty(x.FullName) ? x.FullName : x.Name;
            string yName = !string.IsNullOrEmpty(y.FullName) ? y.FullName : y.Name;
            int order = yName.Count(c => c == '.') - xName.Count(c => c == '.');

            if (order == 0)
            {
                order = string.Compare(xName, yName, StringComparison.Ordinal);
            }

            return order;
        }
    }
}
