using System;
using System.Collections.Generic;
using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Relations
{
    public class AssetRelationProcessor
    {
        public AssetRelationCache Cache { get; }
        public HashSet<string> TargetExtensions { get; }
        public bool ProcessImported { get; set; } = true;
        public bool ProcessDeleted { get; set; } = true;
        public bool ProcessMoved { get; set; }

        public AssetRelationProcessor(string packageName, string cacheName, HashSet<string> targetExtensions) : this(new AssetRelationCache(packageName, cacheName), targetExtensions)
        {
        }

        public AssetRelationProcessor(string path, HashSet<string> targetExtensions) : this(new AssetRelationCache(path), targetExtensions)
        {
        }

        public AssetRelationProcessor(AssetRelationCache cache, HashSet<string> targetExtensions)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            TargetExtensions = targetExtensions ?? throw new ArgumentNullException(nameof(targetExtensions));
        }

        public void Process(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (ProcessImported)
            {
                OnProcessImported(importedAssets);
            }

            if (ProcessDeleted)
            {
                OnProcessDeleted(deletedAssets);
            }

            if (ProcessMoved)
            {
                OnProcessMoved(movedFromAssetPaths, movedAssets);
            }
        }

        protected virtual void OnProcessImported(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];

                if (HasTargetExtension(path))
                {
                    OnProcessImported(path);
                }
            }
        }

        protected virtual void OnProcessImported(string path)
        {
        }

        protected virtual void OnProcessDeleted(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];

                if (HasTargetExtension(path))
                {
                    OnProcessDeleted(path);
                }
            }
        }

        protected virtual void OnProcessDeleted(string path)
        {
            string guid = AssetDatabase.AssetPathToGUID(path);

            if (Cache.Assets.TryGetValue(guid, out HashSet<string> targetChildren))
            {
                foreach (string child in targetChildren)
                {
                    if (Cache.Assets.TryGetValue(child, out HashSet<string> children))
                    {
                        children.Remove(guid);
                    }
                }

                Cache.Assets.Remove(guid);
            }
        }

        protected virtual void OnProcessMoved(string[] fromPaths, string[] toPaths)
        {
            for (int i = 0; i < fromPaths.Length; i++)
            {
                string from = fromPaths[i];
                string to = toPaths[i];

                if (HasTargetExtension(from) || HasTargetExtension(to))
                {
                    OnProcessMoved(from, to);
                }
            }
        }

        protected virtual void OnProcessMoved(string from, string to)
        {
        }

        protected bool HasTargetExtension(string path)
        {
            foreach (string extension in TargetExtensions)
            {
                if (path.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
