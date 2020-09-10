using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static class TypesDropdownEditorUtility
    {
        public static List<DropdownItem<Type>> GetTypeItems(Type targetType, bool useFullPath = false)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(targetType);

            foreach (Type type in types)
            {
                DropdownItem<Type> item = CreateItem(type, useFullPath);

                items.Add(item);
            }

            return items;
        }

        public static DropdownItem<Type> CreateItem(Type type, bool useFullPath = false)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var item = new DropdownItem<Type>(type.Name, type);

            if (useFullPath && !string.IsNullOrEmpty(type.Namespace))
            {
                item.Path = type.Namespace.Replace('.', '/');
            }

            return item;
        }
    }
}
