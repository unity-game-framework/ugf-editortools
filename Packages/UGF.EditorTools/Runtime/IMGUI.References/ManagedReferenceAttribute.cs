using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.References
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ManagedReferenceAttribute : PropertyAttribute
    {
        public Type TargetType { get { return m_targetType ?? throw new ArgumentException("No target type specified."); } }
        public bool HasTargetType { get { return m_targetType != null; } }

        private readonly Type m_targetType;

        public ManagedReferenceAttribute(Type targetType = null)
        {
            m_targetType = targetType;
        }
    }
}
