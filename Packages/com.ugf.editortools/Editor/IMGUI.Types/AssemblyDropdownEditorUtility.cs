using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static class AssemblyDropdownEditorUtility
    {
        public static void GetAssemblyItems(ICollection<DropdownItem<Assembly>> collection, bool useFullPath)
        {
            GetAssemblyItems(collection, _ => true, useFullPath);
        }

        public static void GetAssemblyItems(ICollection<DropdownItem<Assembly>> collection, Func<Assembly, bool> validate, bool useFullPath)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (validate == null) throw new ArgumentNullException(nameof(validate));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                if (validate(assembly))
                {
                    DropdownItem<Assembly> item = CreateItem(assembly, useFullPath);

                    collection.Add(item);
                }
            }
        }

        public static DropdownItem<Assembly> CreateItem(Assembly assembly, bool useFullPath)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var item = new DropdownItem<Assembly>(assembly.GetName().Name, assembly);

            if (useFullPath && item.Name.Contains('.'))
            {
                string path = item.Name.Replace('.', '/');
                string directory = Path.GetDirectoryName(path);

                item.Path = !string.IsNullOrEmpty(directory) ? directory.Replace('\\', '/') : path;
            }

            return item;
        }

        public static void GetAssemblyItems(ICollection<DropdownItem<string>> collection, bool useFullPath)
        {
            GetAssemblyItems(collection, _ => true, useFullPath);
        }

        public static void GetAssemblyItems(ICollection<DropdownItem<string>> collection, Func<Assembly, bool> validate, bool useFullPath)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (validate == null) throw new ArgumentNullException(nameof(validate));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                if (validate(assembly))
                {
                    DropdownItem<string> item = CreateItem2(assembly, useFullPath);

                    collection.Add(item);
                }
            }
        }

        public static DropdownItem<string> CreateItem2(Assembly assembly, bool useFullPath)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var item = new DropdownItem<string>(assembly.GetName().Name, assembly.FullName);

            if (useFullPath && item.Name.Contains('.'))
            {
                string path = item.Name.Replace('.', '/');
                string directory = Path.GetDirectoryName(path);

                item.Path = !string.IsNullOrEmpty(directory) ? directory.Replace('\\', '/') : path;
            }

            return item;
        }
    }
}
