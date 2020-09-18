using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorUtility
    {
        private static readonly char[] m_separator = { '/' };

        public static void SortChildren(AdvancedDropdownItem item, IComparer<AdvancedDropdownItem> comparer, bool recursive = true)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            var children = (List<AdvancedDropdownItem>)item.children;

            children.Sort(comparer);

            if (recursive)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    AdvancedDropdownItem child = children[i];

                    SortChildren(child, comparer);
                }
            }
        }

        public static void AddChild(AdvancedDropdownItem parent, AdvancedDropdownItem child, string path)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (path == null) throw new ArgumentNullException(nameof(path));

            string[] split = path.Split(m_separator, StringSplitOptions.RemoveEmptyEntries);

            AddChild(parent, child, split, 0);
        }

        private static void AddChild(AdvancedDropdownItem parent, AdvancedDropdownItem child, IReadOnlyList<string> path, int index)
        {
            if (index < path.Count)
            {
                string directoryName = path[index];

                if (!TryFindChild(parent, directoryName, out AdvancedDropdownItem directory))
                {
                    directory = new AdvancedDropdownItem(directoryName);

                    parent.AddChild(directory);
                }

                AddChild(directory, child, path, ++index);
            }
            else
            {
                parent.AddChild(child);
            }
        }

        private static bool TryFindChild(AdvancedDropdownItem parent, string name, out AdvancedDropdownItem child)
        {
            var children = (List<AdvancedDropdownItem>)parent.children;

            for (int i = 0; i < children.Count; i++)
            {
                child = children[i];

                if (child.name == name)
                {
                    return true;
                }
            }

            child = null;
            return false;
        }
    }
}
