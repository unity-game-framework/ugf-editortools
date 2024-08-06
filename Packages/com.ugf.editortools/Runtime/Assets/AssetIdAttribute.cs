using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.Assets
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetIdAttribute : PropertyAttribute
    {
        public Type AssetType { get; }

        public AssetIdAttribute(Type assetType = null)
        {
            AssetType = assetType ?? typeof(Object);
        }
    }
}
