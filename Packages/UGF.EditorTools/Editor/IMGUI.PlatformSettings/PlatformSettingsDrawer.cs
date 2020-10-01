﻿using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public class PlatformSettingsDrawer : SettingsGroupsDrawer
    {
        public event SettingsCreatedHandler SettingsCreated;
        public event SettingsDrawingHandler SettingsDrawing;

        public delegate void SettingsCreatedHandler(string name, SerializedProperty propertySettings);
        public delegate void SettingsDrawingHandler(Rect position, SerializedProperty propertySettings, string name);

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
                SettingsCreated.Invoke(name, propertySettings);
            }
            else
            {
                base.OnCreateSettings(propertyGroups, name, propertySettings);
            }
        }

        protected override void OnDrawSettings(Rect position, SerializedProperty propertyGroups, string name)
        {
            if (SettingsDrawing != null)
            {
                SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);

                SettingsDrawing?.Invoke(position, propertySettings, name);
            }
            else
            {
                base.OnDrawSettings(position, propertyGroups, name);
            }
        }
    }
}
