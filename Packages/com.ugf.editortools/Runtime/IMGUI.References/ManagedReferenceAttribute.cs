using System;
using UGF.EditorTools.Runtime.IMGUI.Types;

namespace UGF.EditorTools.Runtime.IMGUI.References
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ManagedReferenceAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get { return m_targetType ?? throw new ArgumentException("Target type not specified."); } }
        public bool HasTargetType { get { return m_targetType != null; } }

        private readonly Type m_targetType;

        public ManagedReferenceAttribute(Type targetType = null)
        {
            m_targetType = targetType;
        }
    }
}
