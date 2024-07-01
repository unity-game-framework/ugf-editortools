using System;
using System.Reflection;

namespace UGF.EditorTools.Editor.Platforms
{
    internal class PlatformBuildPlatformReflection
    {
        public FieldInfo Name { get; }
        public PropertyInfo TargetGroup { get; }
        public FieldInfo Tooltip { get; }
        public FieldInfo DefaultTarget { get; }
        public PropertyInfo Title { get; }
        public PropertyInfo SmallIcon { get; }

        public PlatformBuildPlatformReflection()
        {
            var type = Type.GetType("UnityEditor.Build.BuildPlatform, UnityEditor.CoreModule", true);

            Name = type.GetField("name") ?? throw new ArgumentException("Field not found by the specified name: 'name'.");
            TargetGroup = type.GetProperty("targetGroup") ?? throw new ArgumentException("Property not found by the specified name: 'targetGroup'.");
            Tooltip = type.GetField("tooltip") ?? throw new ArgumentException("Field not found by the specified name: 'tooltip'.");
            DefaultTarget = type.GetField("defaultTarget") ?? throw new ArgumentException("Field not found by the specified name: 'defaultTarget'.");
            Title = type.GetProperty("title") ?? throw new ArgumentException("Property not found by the specified name: 'title'.");
            SmallIcon = type.GetProperty("smallIcon") ?? throw new ArgumentException("Property not found by the specified name: 'smallIcon'.");
        }
    }
}
