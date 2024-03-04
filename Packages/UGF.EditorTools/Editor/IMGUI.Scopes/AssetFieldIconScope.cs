using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct AssetFieldIconScope : IDisposable
    {
        public bool Clicked { get; }

        public AssetFieldIconScope(Rect position, string tooltip)
        {
            Clicked = AttributeEditorGUIUtility.BeginAssetFieldIcon(position, tooltip);
        }

        public void Dispose()
        {
            AttributeEditorGUIUtility.EndAssetFieldIcon();
        }
    }
}
