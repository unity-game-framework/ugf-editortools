using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Dropdown
{
    public class DropdownSelection<TItem> where TItem : DropdownItem
    {
        public int ControlId { get { return m_controlId ?? throw new ArgumentException("Has no control id specified."); } }
        public bool HasControlId { get { return m_controlId.HasValue; } }
        public TItem Selection { get { return m_selection ?? throw new ArgumentException("Has no selection."); } }
        public bool HasSelection { get { return m_selection != null; } }

        private readonly Dropdown<TItem> m_dropdown = new Dropdown<TItem>();
        private int? m_controlId;
        private TItem m_selection;

        public DropdownSelection()
        {
            m_dropdown.Selected += OnDropdownSelected;
        }

        public void Show(Rect position, int controlId, IEnumerable<TItem> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            m_controlId = controlId;
            m_dropdown.Items.Clear();
            m_dropdown.Items.AddRange(items);
            m_dropdown.Show(position);
        }

        public void Clear()
        {
            m_controlId = null;
            m_selection = null;
        }

        public bool TryGet(int controlId, out TItem item)
        {
            if (HasControlId && ControlId == controlId && HasSelection)
            {
                item = Selection;
                return true;
            }

            item = default;
            return false;
        }

        private void OnDropdownSelected(TItem item)
        {
            m_selection = item;
        }
    }
}
