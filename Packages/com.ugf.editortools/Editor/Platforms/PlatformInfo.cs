using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Platforms
{
    public class PlatformInfo
    {
        public string Name { get; }
        public BuildTargetGroup BuildTargetGroup { get; }
        public BuildTarget BuildTarget { get; }
        public GUIContent Label { get; }

        public PlatformInfo(string name, BuildTargetGroup buildTargetGroup, BuildTarget buildTarget, GUIContent label)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BuildTargetGroup = buildTargetGroup;
            BuildTarget = buildTarget;
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }
    }
}
