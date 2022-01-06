using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct LabelWidthMinScope : IDisposable
    {
        private readonly LabelWidthScope m_scope;

        public LabelWidthMinScope(GUIContent label) : this(label, EditorStyles.label)
        {
        }

        public LabelWidthMinScope(GUIContent label, GUIStyle style)
        {
            float size = EditorIMGUIElementsUtility.GetLabelWidth(label, style);

            m_scope = new LabelWidthScope(size);
        }

        public void Dispose()
        {
            m_scope.Dispose();
        }
    }
}
