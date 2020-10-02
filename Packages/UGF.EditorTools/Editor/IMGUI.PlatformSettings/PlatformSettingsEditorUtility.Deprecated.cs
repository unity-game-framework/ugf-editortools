using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public static partial class PlatformSettingsEditorUtility
    {
        private static BuildTargetGroup[] m_buildTargetGroups;

        [Obsolete("GetPlatformsAll has been deprecated. Use BuildTargetGroupsAllAvailable property instead.")]
        public static void GetPlatformsAvailable(ICollection<BuildTargetGroup> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));
            if (m_buildTargetGroups == null) m_buildTargetGroups = GetEnumValues<BuildTargetGroup>().ToArray();

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    BuildTarget buildTarget = GetBuildTarget(buildTargetGroup);

                    if (BuildPipeline.IsBuildTargetSupported(buildTargetGroup, buildTarget))
                    {
                        platforms.Add(buildTargetGroup);
                    }
                }
            }
        }

        [Obsolete("GetPlatformsAll has been deprecated. Use BuildTargetGroupsAll property instead.")]
        public static void GetPlatformsAll(ICollection<BuildTargetGroup> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));
            if (m_buildTargetGroups == null) m_buildTargetGroups = GetEnumValues<BuildTargetGroup>().ToArray();

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    platforms.Add(buildTargetGroup);
                }
            }
        }
    }
}
