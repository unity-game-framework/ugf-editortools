using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct IndentLevelScope : IDisposable
    {
        private readonly int m_indentLevel;

        public IndentLevelScope(int level)
        {
            m_indentLevel = EditorGUI.indentLevel;

            EditorGUI.indentLevel = level;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel = m_indentLevel;
        }
    }
}
