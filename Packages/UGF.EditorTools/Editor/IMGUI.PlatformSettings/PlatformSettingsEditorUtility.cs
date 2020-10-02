using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    public static partial class PlatformSettingsEditorUtility
    {
        public static IReadOnlyList<BuildTargetGroup> BuildTargetGroupsAll { get; }
        public static IReadOnlyList<BuildTargetGroup> BuildTargetGroupsAllAvailable { get; }

        private static readonly Func<BuildTargetGroup, string> m_getBuildTargetGroupDisplayName;

        static PlatformSettingsEditorUtility()
        {
            MethodInfo getBuildTargetGroupDisplayName = typeof(BuildPipeline).GetMethod("GetBuildTargetGroupDisplayName", BindingFlags.Static | BindingFlags.NonPublic)
                                                        ?? throw new ArgumentException("Method not found by the specified name: 'GetBuildTargetGroupDisplayName'.");

            m_getBuildTargetGroupDisplayName = (Func<BuildTargetGroup, string>)getBuildTargetGroupDisplayName.CreateDelegate(typeof(Func<BuildTargetGroup, string>))
                                               ?? throw new ArgumentException($"Can not create delegate from specified method info: '{getBuildTargetGroupDisplayName.Name}'.");

            var all = new List<BuildTargetGroup>();
            var allAvailable = new List<BuildTargetGroup>();
            BuildTargetGroup[] groups = GetEnumValues<BuildTargetGroup>().ToArray();

            foreach (BuildTargetGroup buildTargetGroup in groups)
            {
                if (buildTargetGroup != BuildTargetGroup.Unknown)
                {
                    all.Add(buildTargetGroup);

                    BuildTarget buildTarget = GetBuildTarget(buildTargetGroup);

                    if (BuildPipeline.IsBuildTargetSupported(buildTargetGroup, buildTarget))
                    {
                        allAvailable.Add(buildTargetGroup);
                    }
                }
            }

            BuildTargetGroupsAll = new ReadOnlyCollection<BuildTargetGroup>(all);
            BuildTargetGroupsAllAvailable = new ReadOnlyCollection<BuildTargetGroup>(allAvailable);
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
