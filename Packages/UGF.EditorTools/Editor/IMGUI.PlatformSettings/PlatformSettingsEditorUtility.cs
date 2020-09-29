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
        private static readonly BuildTargetGroup[] m_buildTargetGroups;

        static PlatformSettingsEditorUtility()
        {
            m_buildTargetGroups = GetEnumValues<BuildTargetGroup>().ToArray();
        }

        public static void GetPlatformsAvailable(ICollection<PlatformSettingsInfo> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    BuildTarget buildTarget = GetBuildTarget(buildTargetGroup);

                    if (BuildPipeline.IsBuildTargetSupported(buildTargetGroup, buildTarget))
                    {
                        PlatformSettingsInfo info = GetPlatformInfo(buildTargetGroup);

                        platforms.Add(info);
                    }
                }
            }
        }

        public static void GetPlatformsAll(ICollection<PlatformSettingsInfo> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    PlatformSettingsInfo info = GetPlatformInfo(buildTargetGroup);

                    platforms.Add(info);
                }
            }
        }

        public static PlatformSettingsInfo GetPlatformInfo(BuildTargetGroup targetGroup)
        {
            string groupName = targetGroup.ToString();
            string displayName = ObjectNames.NicifyVariableName(groupName);
            string tooltip = $"{displayName} settings.";

            GUIContent label = TryGetPlatformIcon(targetGroup, out Texture2D texture)
                ? new GUIContent(texture, tooltip)
                : new GUIContent(displayName, tooltip);

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

        public static BuildTarget GetBuildTarget(BuildTargetGroup buildTargetGroup)
        {
            switch (buildTargetGroup)
            {
                case BuildTargetGroup.Unknown: return BuildTarget.NoTarget;
                case BuildTargetGroup.Standalone:
                {
                    switch (Application.platform)
                    {
                        case RuntimePlatform.OSXEditor: return BuildTarget.StandaloneOSX;
                        case RuntimePlatform.WindowsEditor: return BuildTarget.StandaloneWindows;
                        case RuntimePlatform.LinuxEditor: return BuildTarget.StandaloneLinux64;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Application.platform), Application.platform, "Unknown runtime platform.");
                    }
                }
                case BuildTargetGroup.iOS: return BuildTarget.iOS;
                case BuildTargetGroup.Android: return BuildTarget.Android;
                case BuildTargetGroup.WebGL: return BuildTarget.WebGL;
                case BuildTargetGroup.WSA: return BuildTarget.WSAPlayer;
                case BuildTargetGroup.PS4: return BuildTarget.PS4;
                case BuildTargetGroup.XboxOne: return BuildTarget.XboxOne;
                case BuildTargetGroup.tvOS: return BuildTarget.tvOS;
                case BuildTargetGroup.Switch: return BuildTarget.Switch;
                case BuildTargetGroup.Lumin: return BuildTarget.Lumin;
                case BuildTargetGroup.Stadia: return BuildTarget.Stadia;
                case BuildTargetGroup.CloudRendering: return BuildTarget.CloudRendering;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buildTargetGroup), buildTargetGroup, "Unknown build target group.");
            }
        }
    }
}
