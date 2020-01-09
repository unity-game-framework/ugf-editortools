using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public class TypesDropdown : AdvancedDropdown
    {
        public string RootName { get; set; } = "Types";

        public event Action<Type> Selected;

        private readonly Func<IEnumerable<Type>> m_typesCollector;
        private readonly List<Type> m_types = new List<Type>();
        private static readonly char[] m_separator = { '.' };

        public TypesDropdown(Func<IEnumerable<Type>> typesCollector, AdvancedDropdownState state = null) : base(state ?? new AdvancedDropdownState())
        {
            m_typesCollector = typesCollector ?? throw new ArgumentNullException(nameof(typesCollector));

            minimumSize = new Vector2(minimumSize.x, 250F);
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item.id >= 0 && item.id < m_types.Count)
            {
                Selected?.Invoke(m_types[item.id]);
            }
            else
            {
                Selected?.Invoke(null);
            }
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            IEnumerable<Type> types = m_typesCollector();

            m_types.Clear();
            m_types.AddRange(types);
            m_types.Sort(TypesDropdownComparer.Default);

            var root = new AdvancedDropdownItem(RootName);

            root.AddChild(new AdvancedDropdownItem("None")
            {
                id = -1
            });

            for (int i = 0; i < m_types.Count; i++)
            {
                Type type = m_types[i];

                var child = new AdvancedDropdownItem(type.Name)
                {
                    id = i
                };

                IReadOnlyList<string> path = !string.IsNullOrEmpty(type.Namespace)
                    ? type.Namespace.Split(m_separator)
                    : Array.Empty<string>();

                AddMenuItem(root, child, path, 0);
            }

            return root;
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
