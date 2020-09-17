using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct IndentIncrementScope : IDisposable
    {
        private readonly int m_indentLevel;

        public IndentIncrementScope(int value)
        {
            m_indentLevel = EditorGUI.indentLevel;

            EditorGUI.indentLevel += value;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel = m_indentLevel;
        }
    }
}
