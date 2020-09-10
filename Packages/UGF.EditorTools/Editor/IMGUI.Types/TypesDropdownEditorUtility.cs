using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static class TypesDropdownEditorUtility
    {
        private static readonly char[] m_separator = { '.' };

        public static List<DropdownItem<Type>> GetTypeItems(Type targetType, bool useFullPath = false)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(targetType);

            GetTypeItems(types, items, useFullPath);

            items.Sort(TypesDropdownItemsComparer.Default);

            return items;
        }

        public static void GetTypeItems(IEnumerable<Type> types, ICollection<DropdownItem<Type>> items, bool useFullPath = false)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (Type type in types)
            {
                var item = new DropdownItem<Type>(type.Name, type);

                if (useFullPath && TryGetTypePath(type, out string[] path))
                {
                    item.Path = path;
                }

                items.Add(item);
            }
        }

        public static bool TryGetTypePath(Type type, out string[] path)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

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
