using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct AssetFieldIconReferenceScope : IDisposable
    {
        public bool Clicked { get; }

        public AssetFieldIconReferenceScope(Rect position, string label, string value)
        {
            Clicked = AttributeEditorGUIUtility.BeginAssetFieldIconReference(position, label, value);
        }

        public void Dispose()
        {
            AttributeEditorGUIUtility.EndAssetFieldIconReference();
        }
    }
}
