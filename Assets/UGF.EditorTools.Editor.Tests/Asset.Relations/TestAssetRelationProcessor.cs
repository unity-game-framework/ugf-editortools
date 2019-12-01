using System.Collections.Generic;
using UGF.EditorTools.Editor.Asset.Relations;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.Asset.Relations
{
    public class TestAssetRelationProcessor : AssetPostprocessor
    {
        private static readonly AssetRelationProcessor m_postprocessor = new AssetRelationProcessor("Assets/UGF.EditorTools.Editor.Tests/cache.json", new HashSet<string>
        {
            ".mat"
        });

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            m_postprocessor.Process(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
        }
    }
}
