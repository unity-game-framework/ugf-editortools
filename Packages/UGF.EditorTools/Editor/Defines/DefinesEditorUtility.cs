using System;
using System.Collections.Generic;
using UnityEditor;

namespace UGF.EditorTools.Editor.Defines
{
    public static class DefinesEditorUtility
    {
        private static readonly char[] m_separator = { ';' };

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

            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            string[] values = symbols.Split(m_separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in values)
            {
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

            string symbols = string.Join(";", defines);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, symbols);
        }
    }
}
