using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Assets
{
    public static class AssetsEditorUtility
    {
        public static string OpenFileSelection(string directory, string extension, bool inAssets = true)
        {
            return OpenFileSelection("Select File", directory, extension, inAssets);
        }

        public static string OpenFileSelection(string title, string directory, string extension, bool inAssets = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(directory)) throw new ArgumentException("Value cannot be null or empty.", nameof(directory));
            if (string.IsNullOrEmpty(extension)) throw new ArgumentException("Value cannot be null or empty.", nameof(extension));

            string path = EditorUtility.OpenFilePanel(title, directory, extension);

            if (inAssets && !string.IsNullOrEmpty(path))
            {
                path = GetAssetsPath(path);
            }

            return path;
        }

        public static string OpenDirectorySelection(string directory, bool inAssets = true)
        {
            return OpenDirectorySelection("Select Directory", directory, inAssets);
        }

        public static string OpenDirectorySelection(string title, string directory, bool inAssets = true)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Value cannot be null or empty.", nameof(title));
            if (string.IsNullOrEmpty(directory)) throw new ArgumentException("Value cannot be null or empty.", nameof(directory));

            string path = EditorUtility.OpenFolderPanel(title, directory, string.Empty);

            if (inAssets && !string.IsNullOrEmpty(path))
            {
                path = GetAssetsPath(path);
            }

            return path;
        }

        public static string GetAssetsPath(string path)
        {
            return TryGetAssetsPath(path, out string assetsPath) ? assetsPath : throw new ArgumentException($"Assets path not found for specified path: '{path}'.");
        }

        public static bool TryGetAssetsPath(string path, out string assetsPath)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            string assetsDirectory = Application.dataPath;

            if (path.StartsWith(assetsDirectory))
            {
                assetsPath = path.Substring(assetsDirectory.Length + 1, path.Length - assetsDirectory.Length - 1);
                return true;
            }

            assetsPath = string.Empty;
            return false;
        }

        public static string GetResourcesPath(Object asset)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            return TryGetResourcesPath(asset, out string path) ? path : throw new ArgumentException($"No resources path found for specified asset: '{asset}'.");
        }

        public static bool TryGetResourcesPath(Object asset, out string path)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            string assetPath = AssetDatabase.GetAssetPath(asset);

            path = default;
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

            result = default;
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

            result = default;
            return false;
        }
    }
}
