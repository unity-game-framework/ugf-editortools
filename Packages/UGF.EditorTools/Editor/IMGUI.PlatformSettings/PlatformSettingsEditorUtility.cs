using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public static class PlatformSettingsEditorUtility
    {
        private static readonly BuildTarget[] m_buildTargets;
        private static readonly BuildTargetGroup[] m_buildTargetGroups;

        static PlatformSettingsEditorUtility()
        {
            m_buildTargets = GetEnumValues<BuildTarget>().ToArray();
            m_buildTargetGroups = GetEnumValues<BuildTargetGroup>().ToArray();
        }

        public static void GetPlatformsAvailable(ICollection<PlatformSettingsInfo> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

            foreach (BuildTarget buildTarget in m_buildTargets)
            {
                BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(buildTarget);

                if (BuildPipeline.IsBuildTargetSupported(group, buildTarget))
                {
                    PlatformSettingsInfo info = GetPlatformInfo(group);

                    platforms.Add(info);
                }
            }
        }

        public static void GetPlatformsAll(ICollection<PlatformSettingsInfo> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                PlatformSettingsInfo info = GetPlatformInfo(buildTargetGroup);

                platforms.Add(info);
            }
        }

        public static PlatformSettingsInfo GetPlatformInfo(BuildTargetGroup targetGroup)
        {
            string groupName = targetGroup.ToString();
            string displayName = ObjectNames.NicifyVariableName(groupName);
            string tooltip = $"{displayName} settings.";

            GUIContent label = TryGetPlatformIcon(targetGroup, out Texture2D texture)
                ? new GUIContent(texture, tooltip)
                : new GUIContent(groupName, tooltip);

            var info = new PlatformSettingsInfo(groupName, targetGroup, label);

            return info;
        }

        public static bool TryGetPlatformIcon(BuildTargetGroup targetGroup, out Texture2D texture, bool big = false)
        {
            string name = targetGroup.ToString();
            string textureName = big ? $"BuildSettings.{name}" : $"BuildSettings.{name}.Small";

            texture = EditorGUIUtility.FindTexture(textureName);
            return texture != null;
        }

        private static IEnumerable<T> GetEnumValues<T>()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                if (!field.IsDefined(typeof(ObsoleteAttribute)))
                {
                    var value = (T)field.GetValue(null);

                    yield return value;
                }
            }
        }
    }
}
