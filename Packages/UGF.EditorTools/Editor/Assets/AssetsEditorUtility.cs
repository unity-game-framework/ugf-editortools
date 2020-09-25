using System;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetsEditorUtility
    {
        public static string GetResourcesPath(Object asset)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            return TryGetResourcesPath(asset, out string path) ? path : throw new ArgumentException($"No resources path found for specified asset: '{asset}'.");
        }

        public static bool TryGetResourcesPath(Object asset, out string path)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            string assetPath = AssetDatabase.GetAssetPath(asset);

            path = null;
            return !string.IsNullOrEmpty(assetPath) && TryGetResourcesRelativePath(assetPath, out path);
        }

        public static string GetResourcesRelativePath(string path)
        {
            return TryGetResourcesRelativePath(path, out string result) ? result : throw new ArgumentException($"Can not find resources relative path from specified path: '{path}'.");
        }

        public static bool TryGetResourcesRelativePath(string path, out string result)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            if (Path.HasExtension(path))
            {
                path = Path.ChangeExtension(path, null);
            }

            path = path.Replace('\\', '/');

            if (TryGetResourcesSubDirectory(path, out string subDirectory))
            {
                result = subDirectory;
                return true;
            }

            result = null;
            return false;
        }

        private static bool TryGetResourcesSubDirectory(string path, out string result)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            string resources = "resources/";
            int index = path.LastIndexOf(resources, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                index += resources.Length;

                if (index < path.Length)
                {
                    result = path.Substring(index, path.Length - index);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
