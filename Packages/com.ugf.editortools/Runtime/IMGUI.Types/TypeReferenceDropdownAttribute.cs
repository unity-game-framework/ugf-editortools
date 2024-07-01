using System;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    public class TypeReferenceDropdownAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get { return m_targetType ?? throw new ArgumentException("Target type not specified."); } }
        public bool HasTargetType { get { return m_targetType != null; } }

        private readonly Type m_targetType;

        public TypeReferenceDropdownAttribute(Type targetType = null)
        {
            m_targetType = targetType;
        }
    }
}
