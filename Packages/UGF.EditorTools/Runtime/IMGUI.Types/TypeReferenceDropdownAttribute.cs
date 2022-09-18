using System;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    public class TypeReferenceDropdownAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get { return m_targetType ?? throw new ArgumentException("Target type not specified."); } }
        public bool HasTargetType { get { return m_targetType != null; } }
        public Type AttributeType { get { return m_attributeType ?? throw new ArgumentException("Value not specified."); } set { m_attributeType = value ?? throw new ArgumentNullException(nameof(value)); } }
        public bool HasAttributeType { get { return m_attributeType != null; } }

        private readonly Type m_targetType;
        private Type m_attributeType;

        public TypeReferenceDropdownAttribute(Type targetType = null)
        {
            m_targetType = targetType;
        }
    }
}
