﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Runtime.IMGUI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetPathAttribute : PropertyAttribute
    {
        public Type AssetType { get; }

        public AssetPathAttribute(Type assetType = null)
        {
            AssetType = assetType ?? typeof(Object);
        }
    }
}
