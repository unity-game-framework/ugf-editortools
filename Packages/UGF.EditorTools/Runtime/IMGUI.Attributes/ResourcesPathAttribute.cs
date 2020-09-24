using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ResourcesPathAttribute : PropertyAttribute
    {
        public Type AssetType { get; }

        public ResourcesPathAttribute(Type assetType = null)
        {
            AssetType = assetType ?? typeof(Object);
        }
    }
}
