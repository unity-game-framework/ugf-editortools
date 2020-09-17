using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditorInternal;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Yaml
{
    public static class EditorYamlUtility
    {
        public static string ToYaml(Object target)
        {
            using (var scope = new EditorTempScope())
            {
                ToYamlAtPath(target, scope.Path);

                string content = File.ReadAllText(scope.Path);

                return content;
            }
        }

        public static string ToYamlAll(Object[] targets)
        {
            using (var scope = new EditorTempScope())
            {
                ToYamlAllAtPath(targets, scope.Path);

                string content = File.ReadAllText(scope.Path);

                return content;
            }
        }

        public static T FromYaml<T>(string value) where T : Object
        {
            return (T)FromYaml(value);
        }

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

        public static Object[] FromYamlAll(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            using (var scope = new EditorTempScope())
            {
                File.WriteAllText(scope.Path, value);

                Object[] targets = FromYamlAllAtPath(scope.Path);

                return targets;
            }
        }

        public static void ToYamlAtPath(Object target, string path)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            ToYamlAllAtPath(new[] { target }, path);
        }

        public static void ToYamlAllAtPath(Object[] targets, string path)
        {
            if (targets == null) throw new ArgumentNullException(nameof(targets));
            if (targets.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(targets));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            InternalEditorUtility.SaveToSerializedFileAndForget(targets, path, true);
        }

        public static Object FromYamlAtPath(string path)
        {
            Object[] targets = FromYamlAllAtPath(path);

            return targets.Length > 0 ? targets[0] : throw new ArgumentException("No objects loaded from Yaml at specified path.");
        }

        public static Object[] FromYamlAllAtPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            return InternalEditorUtility.LoadSerializedFileAndForget(path);
        }
    }
}
