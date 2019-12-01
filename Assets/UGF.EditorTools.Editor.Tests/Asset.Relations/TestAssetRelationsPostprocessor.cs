using UGF.EditorTools.Editor.Asset.Relations;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.Asset.Relations
{
    public class TestAssetRelationsPostprocessor : AssetPostprocessor
    {
        private static AssetRelationsPostprocessor m_postprocessor = new AssetRelationsPostprocessor("Assets/UGF.EditorTools.Editor.Tests/cache.json");

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            m_postprocessor.Postprocess(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
        }
    }
}
