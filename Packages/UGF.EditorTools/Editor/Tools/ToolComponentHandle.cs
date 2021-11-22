using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tools
{
    public abstract class ToolComponentHandle : ToolComponent
    {
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

        protected virtual void OnHandleDraw()
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

                    OnHandleDraw();

                    if (EditorGUI.EndChangeCheck())
                    {
                        OnHandleChanged();
                    }
                }
            }
        }
    }
}
