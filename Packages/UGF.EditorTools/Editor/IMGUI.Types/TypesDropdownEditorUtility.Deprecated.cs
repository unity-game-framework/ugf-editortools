using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    public static partial class TypesDropdownEditorUtility
    {
        [Obsolete("GetTypeItems() method has been deprecated. Use GetTypeItems() method overload with collection argument instead.")]
        public static List<DropdownItem<Type>> GetTypeItems(Type targetType, bool useFullPath = false)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            var items = new List<DropdownItem<Type>>();

            GetTypeItems(items, targetType, useFullPath, false);

            return items;
        }

        [Obsolete("CreateItem() method has been deprecated. Use CreateItem() method overload with assembly name display argument.")]
        public static DropdownItem<Type> CreateItem(Type type, bool useFullPath = false)
        {
            return CreateItem(type, useFullPath, false);
        }
    }
}
