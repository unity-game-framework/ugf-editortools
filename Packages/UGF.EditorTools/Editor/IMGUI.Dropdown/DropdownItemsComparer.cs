// using System;
// using System.Collections.Generic;
//
// namespace UGF.EditorTools.Editor.IMGUI.Dropdown
// {
//     public class DropdownItemsComparer<TItem> : IComparer<TItem> where TItem : DropdownItem
//     {
//         public static DropdownItemsComparer<TItem> Default { get; } = new DropdownItemsComparer<TItem>();
//
//         public int Compare(TItem x, TItem y)
//         {
//             if (ReferenceEquals(x, y)) return 0;
//             if (ReferenceEquals(null, y)) return 1;
//             if (ReferenceEquals(null, x)) return -1;
//
//             return OnCompare(x, y);
//         }
//
//         protected virtual int OnCompare(TItem x, TItem y)
//         {
//             int order = y.HasPath.CompareTo(x.HasPath);
//
//             if (order == 0)
//             {
//                 string xName = x.HasPath ? $"{x.Path}/{x.Name}" : x.Name;
//                 string yName = y.HasPath ? $"{y.Path}/{y.Name}" : y.Name;
//
//                 order = string.Compare(xName, yName, StringComparison.Ordinal);
//             }
//
//             return order;
//         }
//     }
// }
