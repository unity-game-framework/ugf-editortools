using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public static class DropdownEditorUtility
    {
        private static readonly Dictionary<Type, object> m_handlers = new Dictionary<Type, object>();

        public static DropdownHandler<T> GetDropdownHandler<T>() where T : DropdownItem
        {
            if (!TryGetDropdownHandler(out DropdownHandler<T> handler))
            {
                handler = new DropdownHandler<T>();

                m_handlers.Add(typeof(T), handler);
            }

            return handler;
        }

        public static bool TryGetDropdownHandler<T>(out DropdownHandler<T> handler) where T : DropdownItem
        {
            if (m_handlers.TryGetValue(typeof(T), out object value) && value is DropdownHandler<T> result)
            {
                handler = result;
                return true;
            }

            handler = default;
            return false;
        }

        public static bool TryGetDropdownSelection<T>(int controlId, out T item) where T : DropdownItem
        {
            if (TryGetDropdownHandler(out DropdownHandler<T> handler))
            {
                if (handler.HasControlId && handler.LastControlId == controlId && handler.HasSelectedItem)
                {
                    item = handler.LastSelectedItem;
                    return true;
                }
            }

            item = default;
            return false;
        }

        public static void ClearDropdownSelection<T>() where T : DropdownItem
        {
            if (TryGetDropdownHandler(out DropdownHandler<T> handler))
            {
                handler.Clear();
            }
        }

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
