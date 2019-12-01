using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Relations
{
    public class AssetRelationsPostprocessor
    {
        public AssetRelationCache Cache { get; }

        public AssetRelationsPostprocessor(string packageName, string cacheName)
        {
            Cache = new AssetRelationCache(packageName, cacheName);
        }

        public AssetRelationsPostprocessor(string path)
        {
            Cache = new AssetRelationCache(path);
        }

        public AssetRelationsPostprocessor(AssetRelationCache cache)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public void Postprocess(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
        }

        public virtual void ProcessDeleted(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                string guid = AssetDatabase.AssetPathToGUID(path);
            }
        }
    }
}
