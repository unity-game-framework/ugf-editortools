using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public class PlatformSettingsInfo
    {
        public string Name { get; }
        public BuildTargetGroup BuildTargetGroup { get; }
        public GUIContent Label { get; }

        public PlatformSettingsInfo(string name, BuildTargetGroup buildTargetGroup, GUIContent label)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BuildTargetGroup = buildTargetGroup;
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }
    }
}
