using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class TypesDropdown : AdvancedDropdown
    {
        public event Action<Type> Selected;

        private readonly Func<IEnumerable<Type>> m_typesCollector;
        private readonly Dictionary<int, Type> m_types = new Dictionary<int, Type>();
        private static readonly char[] m_separator = { '.' };

        public TypesDropdown(Func<IEnumerable<Type>> typesCollector, AdvancedDropdownState state = null) : base(state ?? new AdvancedDropdownState())
        {
            m_typesCollector = typesCollector ?? throw new ArgumentNullException(nameof(typesCollector));
            minimumSize = new Vector2(minimumSize.x, 500F);
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            Selected?.Invoke(m_types[item.id]);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            m_types.Clear();

            foreach (Type type in m_typesCollector())
            {
                m_types.Add(type.GetHashCode(), type);
            }

            var root = new AdvancedDropdownItem("Types");

            foreach (KeyValuePair<int, Type> pair in m_types)
            {
                Type type = pair.Value;

                var child = new AdvancedDropdownItem(type.Name)
                {
                    id = pair.Key
                };

                IEnumerable<string> path = !string.IsNullOrEmpty(type.Namespace)
                    ? type.Namespace.Split(m_separator)
                    : Array.Empty<string>();

                AddMenuItem(root, child, path);
            }

            return root;
        }

        private static void AddMenuItem(AdvancedDropdownItem parent, AdvancedDropdownItem child, IEnumerable<string> path)
        {
            int count = path.Count();

            if (count == 0)
            {
                parent.AddChild(child);
            }
            else
            {
                string directoryName = path.First();
                AdvancedDropdownItem directory = null;

                foreach (AdvancedDropdownItem item in parent.children)
                {
                    if (item.name == directoryName)
                    {
                        directory = item;
                        break;
                    }
                }

                if (directory == null)
                {
                    directory = new AdvancedDropdownItem(directoryName);

                    parent.AddChild(directory);
                }

                AddMenuItem(directory, child, path.Skip(1));
            }
        }
    }
}
