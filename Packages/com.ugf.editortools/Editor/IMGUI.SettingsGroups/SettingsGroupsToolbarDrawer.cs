using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.EditorTools.Editor.IMGUI.Toolbar;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsToolbarDrawer : ToolbarDrawer
    {
        public IReadOnlyList<GUIContent> TabLabels { get; }

        private readonly List<GUIContent> m_labels = new List<GUIContent>();

        public SettingsGroupsToolbarDrawer()
        {
            TabLabels = new ReadOnlyCollection<GUIContent>(m_labels);
            Styles = new ToolbarStyles("Tab onlyOne", "Tab first", "Tab middle", "Tab last");
        }

        public void AddLabel(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            m_labels.Add(content);

            Count = m_labels.Count;
        }

        public bool RemoveLabel(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            bool result = m_labels.Remove(content);

            Count = m_labels.Count;

            return result;
        }

        public void ClearLabels()
        {
            m_labels.Clear();

            Count = 0;
        }

        protected override GUIContent OnGetTabLabel(int index)
        {
            if (TabLabels.Count == 0) throw new ArgumentException("Toolbar labels not specified.");
            if (index < 0 || index >= TabLabels.Count) throw new ArgumentOutOfRangeException(nameof(index), $"Toolbar label not found by the specified index: '{index}'.");

            return TabLabels[index];
        }
    }
}
