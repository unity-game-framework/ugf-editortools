using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PopupMenus
{
    public class PopupMenuItem
    {
        public GUIContent Content { get; }
        public bool Enabled { get; set; } = true;
        public bool Priority { get; set; }

        public PopupMenuItem(GUIContent content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
