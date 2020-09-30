using System;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public class PlatformSettingsDrawer : SettingsGroupsDrawer
    {
        public event Action<string, SerializedProperty> SettingsCreated;

        public void AddPlatform(BuildTargetGroup targetGroup)
        {
            string name = targetGroup.ToString();
            GUIContent label = PlatformSettingsEditorUtility.GetPlatformLabel(targetGroup);

            AddGroup(name, label);
        }

        public bool RemovePlatform(BuildTargetGroup targetGroup)
        {
            string name = targetGroup.ToString();
            bool result = RemoveGroup(name);

            return result;
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
