using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetTypeAttribute : PropertyAttribute
    {
        public Type AssetType { get; }

        public AssetTypeAttribute(Type assetType = null)
        {
            AssetType = assetType ?? typeof(Object);
        }
    }
}
