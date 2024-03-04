using System;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct AssetFieldIconScope : IDisposable
    {
        public bool Clicked { get; }

        public AssetFieldIconScope(Rect position, GUIContent content)
        {
            Clicked = AttributeEditorGUIUtility.BeginAssetFieldIcon(position, content);
        }

        public void Dispose()
        {
            AttributeEditorGUIUtility.EndAssetFieldIcon();
        }
    }
}
