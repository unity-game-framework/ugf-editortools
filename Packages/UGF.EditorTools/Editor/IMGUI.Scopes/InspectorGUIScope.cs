using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct InspectorGUIScope : IDisposable
    {
        private readonly bool m_hierarchyMode;
        private readonly bool m_wideMode;
        private readonly int m_indentLevel;
        private readonly float m_labelWidth;

        public InspectorGUIScope(bool enable)
        {
            m_hierarchyMode = EditorGUIUtility.hierarchyMode;
            m_wideMode = EditorGUIUtility.wideMode;
            m_indentLevel = EditorGUI.indentLevel;
            m_labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.hierarchyMode = enable;
            EditorGUIUtility.wideMode = enable;
            EditorGUI.indentLevel++;
            EditorGUIUtility.labelWidth = 0F;
        }

        public void Dispose()
        {
            EditorGUIUtility.hierarchyMode = m_hierarchyMode;
            EditorGUIUtility.wideMode = m_wideMode;
            EditorGUI.indentLevel = m_indentLevel;
            EditorGUIUtility.labelWidth = m_labelWidth;
        }
    }
}
