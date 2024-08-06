using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct GUIBackgroundColorScope : IDisposable
    {
        private readonly Color m_color;

        public GUIBackgroundColorScope(Color color)
        {
            m_color = GUI.backgroundColor;

            GUI.backgroundColor = color;
        }

        public void Dispose()
        {
            GUI.backgroundColor = m_color;
        }
    }
}
