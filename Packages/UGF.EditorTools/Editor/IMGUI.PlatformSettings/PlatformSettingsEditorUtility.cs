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
        private static readonly Func<BuildTargetGroup, string> m_getBuildTargetGroupDisplayName;

        static PlatformSettingsEditorUtility()
        {
            m_buildTargetGroups = GetEnumValues<BuildTargetGroup>().ToArray();

            MethodInfo getBuildTargetGroupDisplayName = typeof(BuildPipeline).GetMethod("GetBuildTargetGroupDisplayName", BindingFlags.Static | BindingFlags.NonPublic)
                                                        ?? throw new ArgumentException("Method not found by the specified name: 'GetBuildTargetGroupDisplayName'.");

            m_getBuildTargetGroupDisplayName = (Func<BuildTargetGroup, string>)getBuildTargetGroupDisplayName.CreateDelegate(typeof(Func<BuildTargetGroup, string>))
                                               ?? throw new ArgumentException($"Can not create delegate from specified method info: '{getBuildTargetGroupDisplayName.Name}'.");
        }

        public static void GetPlatformsAvailable(ICollection<BuildTargetGroup> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

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

        public static void GetPlatformsAll(ICollection<BuildTargetGroup> platforms)
        {
            if (platforms == null) throw new ArgumentNullException(nameof(platforms));

            foreach (BuildTargetGroup buildTargetGroup in m_buildTargetGroups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    platforms.Add(buildTargetGroup);
                }
            }
        }

        public static GUIContent GetPlatformLabel(BuildTargetGroup targetGroup)
        {
            string displayName = GetPlatformDisplayName(targetGroup);
            string tooltip = $"{displayName} settings.";

            GUIContent label = TryGetPlatformIcon(targetGroup, out Texture2D texture)
                ? new GUIContent(texture, tooltip)
                : new GUIContent(displayName, tooltip);

            return label;
        }

        public static string GetPlatformDisplayName(BuildTargetGroup targetGroup)
        {
            return m_getBuildTargetGroupDisplayName(targetGroup);
        }

        public static bool TryGetPlatformIcon(BuildTargetGroup targetGroup, out Texture2D texture, bool big = false)
        {
            string name;

            switch (targetGroup)
            {
                case BuildTargetGroup.WSA:
                {
                    name = big ? "BuildSettings.Metro" : "BuildSettings.Metro.Small";
                    break;
                }
                case BuildTargetGroup.iOS:
                {
                    name = big ? "BuildSettings.Metro" : "BuildSettings.iPhone.Small";
                    break;
                }
                case BuildTargetGroup.CloudRendering:
                {
                    name = big ? "CloudConnect@2x" : "CloudConnect";
                    break;
                }
                default:
                {
                    name = big ? $"BuildSettings.{targetGroup.ToString()}" : $"BuildSettings.{targetGroup.ToString()}.Small";
                    break;
                }
            }

            texture = EditorGUIUtility.FindTexture(name);
            return texture != null;
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
