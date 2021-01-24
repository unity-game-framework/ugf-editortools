using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PlatformSettings
{
    [Obsolete("PlatformSettingsEditorUtility has been deprecated. Use PlatformEditorUtility instead.")]
    public static class PlatformSettingsEditorUtility
    {
        public static IReadOnlyList<BuildTargetGroup> BuildTargetGroupsAll { get; }
        public static IReadOnlyList<BuildTargetGroup> BuildTargetGroupsAllAvailable { get; }

        private static readonly Func<BuildTargetGroup, string> m_getBuildTargetGroupDisplayName;
        private static readonly Func<string, BuildTarget> m_getBuildTargetByName;

        static PlatformSettingsEditorUtility()
        {
            MethodInfo getBuildTargetGroupDisplayName = typeof(BuildPipeline).GetMethod("GetBuildTargetGroupDisplayName", BindingFlags.Static | BindingFlags.NonPublic)
                                                        ?? throw new ArgumentException("Method not found by the specified name: 'GetBuildTargetGroupDisplayName'.");

            MethodInfo getBuildTargetByName = typeof(BuildPipeline).GetMethod("GetBuildTargetByName", BindingFlags.Static | BindingFlags.NonPublic)
                                              ?? throw new ArgumentException("Method not found by the specified name: 'GetBuildTargetByName'.");

            m_getBuildTargetGroupDisplayName = (Func<BuildTargetGroup, string>)getBuildTargetGroupDisplayName.CreateDelegate(typeof(Func<BuildTargetGroup, string>))
                                               ?? throw new ArgumentException($"Can not create delegate from specified method info: '{getBuildTargetGroupDisplayName.Name}'.");

            m_getBuildTargetByName = (Func<string, BuildTarget>)getBuildTargetByName.CreateDelegate(typeof(Func<string, BuildTarget>))
                                     ?? throw new ArgumentException($"Can not create delegate from specified method info: '{getBuildTargetByName.Name}'.");

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
                    name = big ? "BuildSettings.iPhone" : "BuildSettings.iPhone.Small";
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
            return m_getBuildTargetByName(buildTargetGroup.ToString());
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
