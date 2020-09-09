using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static class TypesDropdownEditorUtility
    {
        private static readonly char[] m_separator = { '.' };

        public static List<DropdownItem<Type>> GetTypeItems(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(targetType);

            GetTypeItems(types, items);

            items.Sort(TypesDropdownItemsComparer.Default);

            return items;
        }

        public static void GetTypeItems(IEnumerable<Type> types, ICollection<DropdownItem<Type>> items)
        {
            foreach (Type type in types)
            {
                var item = new DropdownItem<Type>(type.Name, type);

                if (TryGetTypePath(type, out string[] path))
                {
                    item.Path = path;
                }

                items.Add(item);
            }
        }

        public static bool TryGetTypePath(Type type, out string[] path)
        {
            if (!string.IsNullOrEmpty(type.Namespace))
            {
                path = type.Namespace.Split(m_separator);
                return true;
            }

            path = null;
            return false;
        }
    }
}
