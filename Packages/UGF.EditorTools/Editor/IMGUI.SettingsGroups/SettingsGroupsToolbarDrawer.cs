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
            return TabLabels[index];
        }
    }
}
