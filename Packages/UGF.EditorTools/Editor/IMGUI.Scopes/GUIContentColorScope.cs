using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct GUIContentColorScope : IDisposable
    {
        private readonly Color m_color;

        public GUIContentColorScope(Color color)
        {
            m_color = GUI.contentColor;

            GUI.contentColor = color;
        }

        public void Dispose()
        {
            GUI.contentColor = m_color;
        }
    }
}
