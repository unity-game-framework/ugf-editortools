using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tools
{
    public abstract class ToolComponentBoundsHandle<THandle> : ToolComponentHandle where THandle : PrimitiveBoundsHandle
    {
        public THandle Handle { get; }
        public override GUIContent toolbarIcon { get; } = ToolEditorUtility.EditShapeContent;

        protected ToolComponentBoundsHandle(THandle handle)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        protected override void OnHandleDraw()
        {
            base.OnHandleDraw();

            Handle.DrawHandle();
        }
    }
}
