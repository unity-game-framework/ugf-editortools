using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ObjectPickerAttribute : PropertyAttribute
    {
        public Type TargetType { get; }
        public string Filter { get; }

        public ObjectPickerAttribute(string filter = "") : this(typeof(Object), filter)
        {
        }

        public ObjectPickerAttribute(Type targetType, string filter = "")
        {
            TargetType = targetType ?? typeof(Object);
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }
}
