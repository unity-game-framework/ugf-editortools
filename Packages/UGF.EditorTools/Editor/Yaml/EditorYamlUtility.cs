using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Yaml
{
    /// <summary>
    /// Provides utilities to serialize and deserialize Unity objects to Yaml.
    /// <para>
    /// Note: To properly deserialize objects, target class must be defined at the file with the same name as the class name of the target.
    /// This is the same as the requirement for MonoBehaviour classes and files naming.
    /// </para>
    /// </summary>
    public static class EditorYamlUtility
    {
        public static string ToYaml(Object target, bool validate = true)
        {
            using (var scope = new EditorTempScope())
            {
                ToYamlAtPath(target, scope.Path, validate);

                string content = File.ReadAllText(scope.Path);

                return content;
            }
        }

        public static string ToYamlAll(Object[] targets, bool validate = true)
        {
            using (var scope = new EditorTempScope())
            {
                ToYamlAllAtPath(targets, scope.Path, validate);

                string content = File.ReadAllText(scope.Path);

                return content;
            }
        }

        public static T FromYaml<T>(string value) where T : Object
        {
            return (T)FromYaml(value, typeof(T));
        }

        public static Object FromYaml(string value, Type type)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            if (type == null) throw new ArgumentNullException(nameof(type));

            using (var scope = new EditorTempScope())
            {
                File.WriteAllText(scope.Path, value);

                Object target = FromYamlAtPath(scope.Path, type);

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

        public static void ToYamlAtPath(Object target, string path, bool validate = true)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            ToYamlAllAtPath(new[] { target }, path, validate);
        }

        public static void ToYamlAllAtPath(Object[] targets, string path, bool validate = true)
        {
            if (targets == null) throw new ArgumentNullException(nameof(targets));
            if (targets.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(targets));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            if (validate)
            {
                foreach (Object target in targets)
                {
                    if (!ValidateForDeserialization(target))
                    {
                        throw new ArgumentException($"Invalid specified target which can not be deserialized from result: '{target}'.");
                    }
                }
            }

            InternalEditorUtility.SaveToSerializedFileAndForget(targets, path, true);
        }

        public static T FromYamlAtPath<T>(string path) where T : Object
        {
            return (T)FromYamlAtPath(path, typeof(T));
        }

        public static Object FromYamlAtPath(string path, Type type)
        {
            return TryFromYamlAtPath(path, type, out Object target) ? target : throw new ArgumentException($"Target not found from asset at path by the specified type: path:'{path}', type:'{type}'.");
        }

        public static bool TryFromYamlAtPath<T>(string path, out T target) where T : Object
        {
            if (TryFromYamlAtPath(path, typeof(T), out Object value))
            {
                target = (T)value;
                return true;
            }

            target = default;
            return false;
        }

        public static bool TryFromYamlAtPath(string path, Type type, out Object target)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            Object[] targets = FromYamlAllAtPath(path);

            for (int i = 0; i < targets.Length; i++)
            {
                target = targets[i];

                if (type.IsInstanceOfType(target))
                {
                    return true;
                }
            }

            target = default;
            return false;
        }

        public static Object[] FromYamlAllAtPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            return InternalEditorUtility.LoadSerializedFileAndForget(path);
        }

        /// <summary>
        /// Validates the specified object to determines whether it can be properly deserialized.
        /// </summary>
        /// <param name="target">Unity Object to validate.</param>
        /// <remarks>
        /// All MonoBehaviour's and ScriptableObject's must be defined in files with the same name as the name of the class, to support Unity serialization in editor.
        /// This is required to determine type of the object serialization should create and deserialize.
        /// In cases of serialization to text, this is not required, because type of the object already known.
        /// </remarks>
        public static bool ValidateForDeserialization(Object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (target is MonoBehaviour monoBehaviour)
            {
                return MonoScript.FromMonoBehaviour(monoBehaviour) != null;
            }

            if (target is ScriptableObject scriptableObject)
            {
                return MonoScript.FromScriptableObject(scriptableObject) != null;
            }

            return true;
        }
    }
}
