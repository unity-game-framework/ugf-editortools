using System;
using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public class PlatformSettingsDrawer : SettingsGroupsDrawer
    {
        public List<PlatformSettingsInfo> PlatformSettingsInfos { get; } = new List<PlatformSettingsInfo>();

        public event Action<string, SerializedProperty> SettingsCreated;

        public PlatformSettingsDrawer()
        {
            PlatformSettingsEditorUtility.GetPlatformsAll(PlatformSettingsInfos);

            for (int i = 0; i < PlatformSettingsInfos.Count; i++)
            {
                PlatformSettingsInfo info = PlatformSettingsInfos[i];

                Groups.Add(info.Name);
                Toolbar.TabLabels.Add(info.Label);
            }

            Toolbar.Count = PlatformSettingsInfos.Count;
        }

        protected override void OnCreateSettings(SerializedProperty propertyGroups, string name, SerializedProperty propertySettings)
        {
            if (SettingsCreated != null)
            {
                SettingsCreated(name, propertySettings);
            }
            else
            {
                base.OnCreateSettings(propertyGroups, name, propertySettings);
            }
        }
    }
}
