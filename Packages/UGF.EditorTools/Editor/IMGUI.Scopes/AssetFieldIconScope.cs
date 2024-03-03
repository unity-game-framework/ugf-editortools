using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct AssetFieldIconScope : IDisposable
    {
        public bool Result { get; }

        public AssetFieldIconScope(Rect position, string tooltip)
        {
            Result = AttributeEditorGUIUtility.BeginAssetFieldIcon(position, tooltip);
        }

        public void Dispose()
        {
            AttributeEditorGUIUtility.EndAssetFieldIcon();
        }
    }
}
