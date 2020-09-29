using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Toolbar;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.SettingsGroups
{
    public class SettingsGroupsToolbarDrawer : ToolbarDrawer
    {
        public List<GUIContent> TabLabels { get; } = new List<GUIContent>();

        public SettingsGroupsToolbarDrawer()
        {
            Styles = new ToolbarStyles("Tab onlyOne", "Tab first", "Tab middle", "Tab last");
        }

        protected override GUIContent OnGetTabLabel(int index)
        {
            if (TabLabels.Count == 0) throw new ArgumentException("Toolbar labels not specified.");
            if (index < 0 || index >= TabLabels.Count) throw new ArgumentOutOfRangeException(nameof(index), $"Toolbar label not found by the specified index: '{index}'.");

            return TabLabels[index];
        }
    }
}
