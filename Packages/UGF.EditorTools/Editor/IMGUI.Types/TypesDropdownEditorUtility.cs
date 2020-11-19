using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static partial class TypesDropdownEditorUtility
    {
        public static void GetTypeItems(ICollection<DropdownItem<Type>> collection, Func<Type, bool> validate, bool useFullPath, bool displayAssemblyName)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (validate == null) throw new ArgumentNullException(nameof(validate));

            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<object>();

            foreach (Type type in types)
            {
                if (validate(type))
                {
                    DropdownItem<Type> item = CreateItem(type, useFullPath, displayAssemblyName);

                    collection.Add(item);
                }
            }
        }

        public static void GetTypeItems(ICollection<DropdownItem<Type>> collection, Type targetType, bool useFullPath, bool displayAssemblyName)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(targetType);

            foreach (Type type in types)
            {
                DropdownItem<Type> item = CreateItem(type, useFullPath, displayAssemblyName);

                collection.Add(item);
            }
        }

        public static DropdownItem<Type> CreateItem(Type type, bool useFullPath, bool displayAssemblyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            string name = GetTypeDisplayName(type, displayAssemblyName);
            var item = new DropdownItem<Type>(name, type);

            if (useFullPath && !string.IsNullOrEmpty(type.Namespace))
            {
                item.Path = type.Namespace.Replace('.', '/');
            }

            return item;
        }

        public static string GetTypeDisplayName(Type type, bool displayAssemblyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            string name = type.IsNested ? $"{GetTypeDisplayName(type.DeclaringType, false)}+{type.Name}" : type.Name;

            if (displayAssemblyName)
            {
                name = $"{name} ({type.Assembly.GetName().Name})";
            }

            return name;
        }
    }
}
