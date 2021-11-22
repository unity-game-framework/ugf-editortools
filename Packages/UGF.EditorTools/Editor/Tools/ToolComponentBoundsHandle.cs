using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tools
{
    public abstract class ToolComponentBoundsHandle<THandle> : ToolComponent where THandle : PrimitiveBoundsHandle
    {
        public THandle Handle { get; }
        public override GUIContent toolbarIcon { get; } = ToolEditorUtility.EditShapeContent;

        protected ToolComponentBoundsHandle(THandle handle)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        protected virtual Matrix4x4 OnGetMatrix()
        {
            return Component.transform.localToWorldMatrix;
        }

        protected virtual void OnHandleSetup()
        {
        }

        protected virtual void OnHandleChanged()
        {
        }

        protected override void OnToolGUI()
        {
            using (new SerializedObjectUpdateScope(SerializedObject))
            {
                Matrix4x4 matrix = OnGetMatrix();

                using (new Handles.DrawingScope(matrix))
                {
                    OnHandleSetup();

                    EditorGUI.BeginChangeCheck();

                    Handle.DrawHandle();

                    if (EditorGUI.EndChangeCheck())
                    {
                        OnHandleChanged();
                    }
                }
            }
        }
    }
}
