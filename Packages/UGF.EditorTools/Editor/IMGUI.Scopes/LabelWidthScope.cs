using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct LabelWidthScope : IDisposable
    {
        private readonly float m_width;

        public LabelWidthScope(float width)
        {
            m_width = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = width;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = m_width;
        }
    }
}
