using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.Types
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssemblyReferenceDropdownAttribute : PropertyAttribute
    {
        public bool DisplayFullPath { get; set; } = true;
    }
}
