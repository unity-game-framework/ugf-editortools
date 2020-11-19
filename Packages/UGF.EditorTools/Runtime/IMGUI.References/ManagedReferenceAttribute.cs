using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.References
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ManagedReferenceAttribute : PropertyAttribute
    {
        public Type TargetType { get { return m_targetType ?? throw new ArgumentException("No target type specified."); } }
        public bool HasTargetType { get { return m_targetType != null; } }
        public bool DisplayFullPath { get; }
        public bool DisplayAssemblyName { get; set; }

        private readonly Type m_targetType;

        public ManagedReferenceAttribute(Type targetType = null, bool displayFullPath = false)
        {
            m_targetType = targetType;

            DisplayFullPath = displayFullPath;
        }
    }
}
