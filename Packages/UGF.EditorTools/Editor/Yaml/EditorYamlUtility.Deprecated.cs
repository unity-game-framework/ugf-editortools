using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Yaml
{
    public static partial class EditorYamlUtility
    {
        [Obsolete("FromYaml has been deprecated. Use FromYaml method override with specified type of the target.")]
        public static Object FromYaml(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            using (var scope = new EditorTempScope())
            {
                File.WriteAllText(scope.Path, value);

                Object target = FromYamlAtPath(scope.Path);

                return target;
            }
        }

        [Obsolete("FromYamlAtPath has been deprecated. Use FromYamlAtPath method override with specified type of the target.")]
        public static Object FromYamlAtPath(string path)
        {
            Object[] targets = FromYamlAllAtPath(path);

            return targets.Length > 0 ? targets[0] : throw new ArgumentException("No objects loaded from Yaml at specified path.");
        }
    }
}
