using System;
using System.Reflection;

namespace UGF.EditorTools.Editor.Platforms
{
    internal class PlatformBuildPlatformsReflection
    {
        public PropertyInfo Instance { get; }
        public FieldInfo BuildPlatforms { get; }

        public PlatformBuildPlatformsReflection()
        {
            var type = Type.GetType("UnityEditor.Build.BuildPlatforms, UnityEditor.CoreModule", true);

            Instance = type.GetProperty("instance", BindingFlags.Static | BindingFlags.Public)
                       ?? throw new ArgumentException($"Property not found in specified type: '{type}'.");

            BuildPlatforms = type.GetField("buildPlatforms", BindingFlags.Instance | BindingFlags.Public)
                             ?? throw new ArgumentException($"Field not found in specified type: '{type}'.");
        }
    }
}
