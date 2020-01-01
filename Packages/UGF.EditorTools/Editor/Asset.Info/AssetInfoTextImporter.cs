using UnityEngine;

namespace UGF.EditorTools.Editor.Asset.Info
{
    public abstract class AssetInfoTextImporter<TInfo> : AssetInfoImporter<TInfo> where TInfo : class, IAssetInfo, new()
    {
        protected override Object OnCreateAsset(TInfo info)
        {
            string text = OnCreateTextAsset(info);
            var asset = new TextAsset(text);

            return asset;
        }

        protected abstract string OnCreateTextAsset(TInfo info);
    }
}
