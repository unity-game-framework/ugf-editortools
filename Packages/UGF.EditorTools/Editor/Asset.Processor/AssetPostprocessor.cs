using UGF.EditorTools.Editor.Asset.Processor.Settings;

namespace UGF.EditorTools.Editor.Asset.Processor
{
    internal class AssetPostprocessor : UnityEditor.AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (AssetProcessorSettings.Active)
            {
            }
        }
    }
}
