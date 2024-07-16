using System;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ListAttribute : PropertyAttribute
    {
        public ListAttribute() : base(true)
        {
        }
    }
}
