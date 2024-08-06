using System;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.UIToolkit.Types;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine.UIElements;

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

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new TypesDropdownFieldElement(property, true)
            {
                label = preferredLabel,
                TargetType = Attribute.TargetType
            };
        }
    }
}
