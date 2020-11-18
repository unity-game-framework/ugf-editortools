using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    [CustomPropertyDrawer(typeof(TypesDropdownAttribute), true)]
    internal class TypesDropdownAttributePropertyDrawer : TypesDropdownAttributePropertyDrawer<TypesDropdownAttribute>
    {
        public TypesDropdownAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override DropdownDrawer<DropdownItem<Type>> OnCreateDrawer()
        {
            return new TypesDropdownDrawer(GetItems);
        }

        protected override void OnGetItems(ICollection<DropdownItem<Type>> items)
        {
            base.OnGetItems(items);

            TypesDropdownEditorUtility.GetTypeItems(items, Attribute.TargetType, Attribute.DisplayFullPath, Attribute.DisplayAssemblyName);
        }
    }
}
