using System;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownItem
    {
        public string Name { get; }
        public bool Enabled { get; set; }
        public string Path { get { return m_path ?? throw new ArgumentException("No path specified."); } set { m_path = value; } }
        public bool HasPath { get { return m_path != null; } }
        public Texture2D Icon { get { return m_icon != null ? m_icon : throw new ArgumentException("No icon specified."); } set { m_icon = value; } }
        public bool HasIcon { get { return m_icon != null; } }

        private string m_path;
        private Texture2D m_icon;

        public DropdownItem(string name = "Item", bool enabled = true)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Enabled = enabled;
        }
    }
}
