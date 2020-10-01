using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
﻿using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    [CustomPropertyDrawer(typeof(PlatformSettings<>), true)]
    public class PlatformSettingsPropertyDrawer : PropertyDrawerBase
    {
        protected PlatformSettingsDrawer Drawer { get; set; } = new PlatformSettingsDrawer();

        public PlatformSettingsPropertyDrawer()
        {
            var platforms = new List<BuildTargetGroup>();

            PlatformSettingsEditorUtility.GetPlatformsAvailable(platforms);

            for (int i = 0; i < platforms.Count; i++)
            {
                BuildTargetGroup platform = platforms[i];

                Drawer.AddPlatform(platform);
            }
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");

            Drawer.DrawGUI(position, propertyGroups);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");

            return Drawer.GetHeight(propertyGroups);
        }
    }
}
