using System;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TypesDropdownAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get; }

        public TypesDropdownAttribute(Type targetType = null)
        {
            TargetType = targetType ?? typeof(object);
        }
    }
}
