using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Platforms
{
    public static class PlatformEditorUtility
    {
        public static IReadOnlyList<PlatformInfo> PlatformsAll { get; }
        public static IReadOnlyList<PlatformInfo> PlatformsAllAvailable { get; }

        private static readonly PlatformBuildPlatformsReflection m_buildPlatformsReflection = new PlatformBuildPlatformsReflection();
        private static readonly PlatformBuildPlatformReflection m_buildPlatformReflection = new PlatformBuildPlatformReflection();

        static PlatformEditorUtility()
        {
            var all = new List<PlatformInfo>();
            var available = new List<PlatformInfo>();

            InternalGetPlatforms(all);

            for (int i = 0; i < all.Count; i++)
            {
                PlatformInfo platform = all[i];

                if (BuildPipeline.IsBuildTargetSupported(platform.BuildTargetGroup, platform.BuildTarget))
                {
                    available.Add(platform);
                }
            }

            PlatformsAll = new ReadOnlyCollection<PlatformInfo>(all);
            PlatformsAllAvailable = new ReadOnlyCollection<PlatformInfo>(available);
        }

        public static PlatformInfo GetPlatform(BuildTargetGroup buildTargetGroup)
        {
            return TryGetPlatform(buildTargetGroup, out PlatformInfo platform) ? platform : throw new ArgumentException($"Platform not found by the specified build target group: '{buildTargetGroup}'.");
        }

        public static bool TryGetPlatform(BuildTargetGroup buildTargetGroup, out PlatformInfo platform)
        {
            for (int i = 0; i < PlatformsAll.Count; i++)
            {
                platform = PlatformsAll[i];

                if (platform.BuildTargetGroup == buildTargetGroup)
                {
                    return true;
                }
            }

            platform = default;
            return false;
        }

        public static PlatformInfo GetPlatform(BuildTarget buildTarget)
        {
            return TryGetPlatform(buildTarget, out PlatformInfo platform) ? platform : throw new ArgumentException($"Platform not found by the specified build target: '{buildTarget}'.");
        }

        public static bool TryGetPlatform(BuildTarget buildTarget, out PlatformInfo platform)
        {
            for (int i = 0; i < PlatformsAll.Count; i++)
            {
                platform = PlatformsAll[i];

                if (platform.BuildTarget == buildTarget)
                {
                    return true;
                }
            }

            platform = default;
            return false;
        }

        private static void InternalGetPlatforms(ICollection<PlatformInfo> platforms)
        {
            object[] values = InternalGetPlatforms();

            for (int i = 0; i < values.Length; i++)
            {
                object value = values[i];
                PlatformInfo info = InternalGetPlatform(value);

                platforms.Add(info);
            }
        }

        private static object[] InternalGetPlatforms()
        {
            object instance = m_buildPlatformsReflection.Instance.GetValue(null);
            object platforms = m_buildPlatformsReflection.BuildPlatforms.GetValue(instance);

            return (object[])platforms;
        }

        private static PlatformInfo InternalGetPlatform(object value)
        {
            string name = (string)m_buildPlatformReflection.Name.GetValue(value);
            var buildTargetGroup = (BuildTargetGroup)m_buildPlatformReflection.TargetGroup.GetValue(value);
            string tooltip = (string)m_buildPlatformReflection.Tooltip.GetValue(value);
            var buildTarget = (BuildTarget)m_buildPlatformReflection.DefaultTarget.GetValue(value);
            var title = (GUIContent)m_buildPlatformReflection.Title.GetValue(value);
            var icon = (Texture2D)m_buildPlatformReflection.SmallIcon.GetValue(value);
            var label = new GUIContent(title.text, icon, tooltip);

            var info = new PlatformInfo(name, buildTargetGroup, buildTarget, label);

            return info;
        }
    }
}
