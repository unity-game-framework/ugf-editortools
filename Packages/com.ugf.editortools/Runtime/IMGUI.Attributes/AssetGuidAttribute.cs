using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetGuidAttribute : PropertyAttribute
    {
        public Type AssetType { get; }

        public AssetGuidAttribute(Type assetType = null)
        {
            AssetType = assetType ?? typeof(Object);
        }
    }
}
