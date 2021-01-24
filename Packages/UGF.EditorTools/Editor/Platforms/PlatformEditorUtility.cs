using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Platforms
{
    public static class PlatformEditorUtility
    {
        public static IReadOnlyList<PlatformInfo> Platforms { get; }

        private static readonly List<PlatformInfo> m_platforms = new List<PlatformInfo>();
        private static readonly PlatformBuildPlatformsReflection m_buildPlatformsReflection = new PlatformBuildPlatformsReflection();
        private static readonly PlatformBuildPlatformReflection m_buildPlatformReflection = new PlatformBuildPlatformReflection();

        static PlatformEditorUtility()
        {
            Platforms = new ReadOnlyCollection<PlatformInfo>(m_platforms);

            InternalGetPlatforms(m_platforms);
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
