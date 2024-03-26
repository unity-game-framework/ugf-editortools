using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public struct MixedValueChangedScope : IDisposable
    {
        public bool Changed { get { return m_changed ??= EditorGUI.EndChangeCheck(); } }

        private readonly bool m_isMixed;
        private bool? m_changed;

        public MixedValueChangedScope(bool isMixed)
        {
            m_isMixed = EditorGUI.showMixedValue;
            m_changed = default;

            EditorGUI.showMixedValue = isMixed;
            EditorGUI.BeginChangeCheck();
        }

        public void Dispose()
        {
            EditorGUI.showMixedValue = m_isMixed;

            m_changed ??= EditorGUI.EndChangeCheck();
        }
    }
}
