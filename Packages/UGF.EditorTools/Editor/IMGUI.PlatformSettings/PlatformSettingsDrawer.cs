using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UGF.EditorTools.Editor.Platforms;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public class PlatformSettingsDrawer : SettingsGroupsWithTypesDrawer
    {
        public bool AutoSettingsInstanceCreation { get; set; }

        public event SettingsCreatedHandler SettingsCreated;
        public event SettingsDrawingHandler SettingsDrawing;

        public delegate void SettingsCreatedHandler(string name, SerializedProperty propertySettings);
        public delegate void SettingsDrawingHandler(Rect position, SerializedProperty propertySettings, string name);

        public void AddPlatformAllAvailable()
        {
            for (int i = 0; i < PlatformEditorUtility.PlatformsAllAvailable.Count; i++)
            {
                PlatformInfo platform = PlatformEditorUtility.PlatformsAllAvailable[i];

                AddPlatform(platform.BuildTargetGroup);
            }
        }

        public void AddPlatformAll()
        {
            for (int i = 0; i < PlatformEditorUtility.PlatformsAll.Count; i++)
            {
                PlatformInfo platform = PlatformEditorUtility.PlatformsAll[i];

                AddPlatform(platform.BuildTargetGroup);
            }
        }

        public void AddPlatformAll(IReadOnlyList<BuildTargetGroup> targetGroups)
        {
            for (int i = 0; i < targetGroups.Count; i++)
            {
                AddPlatform(targetGroups[i]);
            }
        }

        public void AddPlatform(BuildTargetGroup targetGroup)
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(targetGroup);
            string name = targetGroup.ToString();
            var label = new GUIContent(platform.Label.image, platform.Label.tooltip);

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
                if (AutoSettingsInstanceCreation)
                {
                    base.OnCreateSettings(propertyGroups, name, propertySettings);
                }
                else
                {
                    propertySettings.managedReferenceValue = null;
                    propertySettings.serializedObject.ApplyModifiedProperties();
                }
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
