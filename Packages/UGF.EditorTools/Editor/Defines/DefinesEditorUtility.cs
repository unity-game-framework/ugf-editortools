using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace UGF.EditorTools.Editor.Defines
{
    public static class DefinesEditorUtility
    {
        public static bool HasDefine(string define, BuildTargetGroup buildTargetGroup)
        {
            if (string.IsNullOrEmpty(define)) throw new ArgumentException("Value cannot be null or empty.", nameof(define));

            var defines = new HashSet<string>();

            GetDefines(defines, buildTargetGroup);

            return defines.Contains(define);
        }

        public static void GetDefines(ICollection<string> defines, BuildTargetGroup buildTargetGroup)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);

            PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out string[] values);

            for (int i = 0; i < values.Length; i++)
            {
                string value = values[i];

                defines.Add(value);
            }
        }

        public static bool RemoveDefine(string define, BuildTargetGroup buildTargetGroup)
        {
            if (string.IsNullOrEmpty(define)) throw new ArgumentException("Value cannot be null or empty.", nameof(define));

            var defines = new HashSet<string>();

            GetDefines(defines, buildTargetGroup);

            bool changed = defines.Remove(define);

            SetDefines(defines, buildTargetGroup);

            return changed;
        }

        public static bool SetDefine(string define, BuildTargetGroup buildTargetGroup)
        {
            if (string.IsNullOrEmpty(define)) throw new ArgumentException("Value cannot be null or empty.", nameof(define));

            var defines = new HashSet<string>();

            GetDefines(defines, buildTargetGroup);

            bool changed = defines.Add(define);

            SetDefines(defines, buildTargetGroup);

            return changed;
        }

        public static void SetDefines(IEnumerable<string> defines, BuildTargetGroup buildTargetGroup)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);

            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, defines.ToArray());
        }
    }
}
