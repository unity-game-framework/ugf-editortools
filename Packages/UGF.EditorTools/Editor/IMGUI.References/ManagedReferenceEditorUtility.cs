using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public static class ManagedReferenceEditorUtility
    {
        private static readonly char[] m_separator = { ' ' };

        public static List<DropdownItem<Type>> GetTypeItems(Type targetType)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(targetType);

            foreach (Type type in types)
            {
                if (IsValidType(type))
                {
                    var item = new DropdownItem<Type>(type.Name, type);

                    if (TypesDropdownEditorUtility.TryGetTypePath(type, out string[] path))
                    {
                        item.Path = path;
                    }

                    items.Add(item);
                }
            }

            items.Sort(TypesDropdownItemsComparer.Default);

            return items;
        }

        public static bool TryGetType(string managedReferenceTypeName, out Type type)
        {
            if (managedReferenceTypeName == null) throw new ArgumentNullException(nameof(managedReferenceTypeName));

            if (!string.IsNullOrEmpty(managedReferenceTypeName))
            {
                string[] split = managedReferenceTypeName.Split(m_separator);
                string typeName = split.Length > 1 ? $"{split[1]}, {split[0]}" : split[0];

                type = Type.GetType(typeName);

                return type != null;
            }

            type = null;
            return false;
        }

        public static bool IsValidType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return !type.IsAbstract
                   && !type.IsGenericType
                   && type.IsDefined(typeof(SerializableAttribute))
                   && !typeof(Object).IsAssignableFrom(type)
                   && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
