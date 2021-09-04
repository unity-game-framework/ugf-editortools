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
        public bool DisplayPlatformName { get; set; } = true;

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
            var label = new GUIContent(platform.Label.image, platform.Label.tooltip);

            AddGroup(platform.Name, label);
        }

        public bool RemovePlatform(BuildTargetGroup targetGroup)
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(targetGroup);
            bool result = RemoveGroup(platform.Name);

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
                if (DisplayPlatformName)
                {
                    float height = EditorGUIUtility.singleLineHeight;
                    float space = EditorGUIUtility.standardVerticalSpacing;

                    var rectPlatformName = new Rect(position.x, position.y, position.width, height);
                    var rectSettings = new Rect(position.x, rectPlatformName.yMax + space, position.width, position.height - height - space);

                    OnDrawSettingsPlatformName(rectPlatformName, propertyGroups, name);

                    base.OnDrawSettings(rectSettings, propertyGroups, name);
                }
                else
                {
                    base.OnDrawSettings(position, propertyGroups, name);
                }
            }
        }

        protected override float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            return DisplayPlatformName ? base.OnGetSettingsHeight(propertyGroups) + height + space : base.OnGetSettingsHeight(propertyGroups);
        }

        protected virtual void OnDrawSettingsPlatformName(Rect position, SerializedProperty propertyGroups, string name)
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(name);

            EditorGUI.LabelField(position, $"Settings for {platform.Label.text}");
        }
    }
}
