using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownItem
    {
        public string Name { get; set; } = "Item";
        public bool Enabled { get; set; } = true;
        public IReadOnlyList<string> Path { get { return m_path ?? throw new ArgumentException("No path specified."); } set { m_path = value; } }
        public bool HasPath { get { return m_path != null; } }
        public Texture2D Icon { get { return m_icon != null ? m_icon : throw new ArgumentException("No icon specified."); } set { m_icon = value; } }
        public bool HasIcon { get { return m_icon != null; } }

        private IReadOnlyList<string> m_path;
        private Texture2D m_icon;
    }
}
