using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct SerializedPropertyContextMenuScope : IDisposable
    {
        private readonly EditorApplication.SerializedPropertyCallbackFunction m_handler;

        public SerializedPropertyContextMenuScope(EditorApplication.SerializedPropertyCallbackFunction handler)
        {
            m_handler = handler ?? throw new ArgumentNullException(nameof(handler));

            EditorApplication.contextualPropertyMenu += m_handler;
        }

        public void Dispose()
        {
            EditorApplication.contextualPropertyMenu -= m_handler;
        }
    }
}
