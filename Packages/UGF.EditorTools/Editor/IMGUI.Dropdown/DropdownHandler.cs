using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownHandler<TItem> where TItem : DropdownItem
    {
        public int LastControlId { get { return m_lastControlId ?? throw new ArgumentException("No control id specified."); } }
        public bool HasControlId { get { return m_lastControlId.HasValue; } }
        public TItem LastSelectedItem { get { return m_lastSelectedItem ?? throw new ArgumentException("No selected item."); } }
        public bool HasSelectedItem { get { return m_lastSelectedItem != null; } }

        private readonly Dropdown<TItem> m_dropdown = new Dropdown<TItem>();
        private int? m_lastControlId;
        private TItem m_lastSelectedItem;

        public DropdownHandler()
        {
            m_dropdown.Selected += OnDropdownSelected;
        }

        public void Show(Rect position, int controlId, IEnumerable<TItem> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            m_lastControlId = controlId;
            m_dropdown.Items.Clear();
            m_dropdown.Items.AddRange(items);
            m_dropdown.Show(position);
        }

        public void Clear()
        {
            m_lastControlId = null;
            m_lastSelectedItem = null;
        }

        private void OnDropdownSelected(TItem item)
        {
            m_lastSelectedItem = item;
        }
    }
}
