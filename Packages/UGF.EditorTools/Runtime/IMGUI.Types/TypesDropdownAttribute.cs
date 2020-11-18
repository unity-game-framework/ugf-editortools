using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TypesDropdownAttribute : PropertyAttribute
    {
        public Type TargetType { get; }
        public bool DisplayFullPath { get; set; } = true;
        public bool DisplayAssemblyName { get; set; }

        public TypesDropdownAttribute(Type targetType = null)
        {
            TargetType = targetType ?? typeof(object);
        }
    }
}
