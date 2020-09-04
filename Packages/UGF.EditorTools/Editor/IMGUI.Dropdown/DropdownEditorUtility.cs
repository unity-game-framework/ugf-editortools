using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorUtility
    {
        public static void AddMenuItem(AdvancedDropdownItem parent, AdvancedDropdownItem child, IReadOnlyList<string> path)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (path == null) throw new ArgumentNullException(nameof(path));

            AddMenuItem(parent, child, path, 0);
        }

        private static void AddMenuItem(AdvancedDropdownItem parent, AdvancedDropdownItem child, IReadOnlyList<string> path, int index)
        {
            if (index < path.Count)
            {
                string directoryName = path[index];

                if (!TryFindItem(parent, directoryName, out AdvancedDropdownItem directory))
                {
                    directory = new AdvancedDropdownItem(directoryName);

                    parent.AddChild(directory);
                }

                AddMenuItem(directory, child, path, ++index);
            }
            else
            {
                parent.AddChild(child);
            }
        }

        private static bool TryFindItem(AdvancedDropdownItem parent, string name, out AdvancedDropdownItem item)
        {
            foreach (AdvancedDropdownItem child in parent.children)
            {
                if (child.name == name)
                {
                    item = child;
                    return true;
                }
            }

            item = null;
            return false;
        }
    }
}
