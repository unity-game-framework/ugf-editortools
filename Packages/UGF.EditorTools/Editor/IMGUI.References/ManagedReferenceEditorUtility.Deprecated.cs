using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Types;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.References
{
    public static partial class ManagedReferenceEditorUtility
    {
        [Obsolete("GetTypeItems method has been deprecated. Use TypesDropdownEditorUtility.GetTypeItems instead.")]
        public static List<DropdownItem<Type>> GetTypeItems(Type targetType, bool useFullPath = false)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<object>();

            foreach (Type type in types)
            {
                if (targetType.IsAssignableFrom(type) && IsValidType(type))
                {
                    DropdownItem<Type> item = TypesDropdownEditorUtility.CreateItem(type, useFullPath, false);

                    items.Add(item);
                }
            }

            return items;
        }
    }
}
