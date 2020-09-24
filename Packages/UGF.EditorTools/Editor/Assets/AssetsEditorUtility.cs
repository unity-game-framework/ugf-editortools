using System;
using System.IO;
using System.Text;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetsEditorUtility
    {
        public static string GetResourcesRelativePath(string path)
        {
            return TryGetResourcesRelativePath(path, out string result) ? result : throw new ArgumentException($"Can not find resources relative path from specified path: '{path}'.");
        }

        public static bool TryGetResourcesRelativePath(string path, out string result)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            var builder = new StringBuilder();
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(directory))
            {
                string resources = "resources";
                int index = directory.LastIndexOf(resources, StringComparison.OrdinalIgnoreCase);

                index += resources.Length + 1;

                if (index < directory.Length)
                {
                    int length = directory.Length - index;

                    if (length > 0)
                    {
                        directory = directory.Substring(index, length);
                        directory = directory.Replace('\\', '/');

                        builder.Append(directory);

                        if (!string.IsNullOrEmpty(name))
                        {
                            builder.Append('/');
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                builder.Append(name);
            }

            if (builder.Length > 0)
            {
                result = builder.ToString();
                return true;
            }

            result = null;
            return false;
        }
    }
}
