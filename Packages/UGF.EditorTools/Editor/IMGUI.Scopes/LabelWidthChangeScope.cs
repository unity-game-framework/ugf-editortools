using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct LabelWidthChangeScope : IDisposable
    {
        private readonly float m_labelWidth;

        public LabelWidthChangeScope(float change, bool clearIndent = false)
        {
            m_labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth += change;

            if (clearIndent)
            {
                EditorGUIUtility.labelWidth -= EditorIMGUIUtility.GetIndent();
            }
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = m_labelWidth;
        }
    }
}
