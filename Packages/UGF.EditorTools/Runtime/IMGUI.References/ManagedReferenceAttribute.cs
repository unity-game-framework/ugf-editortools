using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.References
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ManagedReferenceAttribute : PropertyAttribute
    {
        public Type TargetType { get; }

        public ManagedReferenceAttribute(Type targetType = null)
        {
            TargetType = targetType ?? typeof(object);
        }
    }
}
