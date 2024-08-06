using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class TypesDropdownAttributeBase : PropertyAttribute
    {
        public abstract Type TargetType { get; }
        public bool DisplayFullPath { get; set; } = true;
        public bool DisplayAssemblyName { get; set; }
    }
}
