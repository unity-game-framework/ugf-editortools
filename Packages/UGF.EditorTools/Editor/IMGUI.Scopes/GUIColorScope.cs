using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct GUIColorScope : IDisposable
    {
        private readonly Color m_color;

        public GUIColorScope(Color color)
        {
            m_color = GUI.color;

            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = m_color;
        }
    }
}
