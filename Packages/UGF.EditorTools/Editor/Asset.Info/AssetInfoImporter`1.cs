using System;
using UnityEditor.Experimental.AssetImporters;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Asset.Info
{
    public abstract class AssetInfoImporter<TInfo> : AssetInfoImporter where TInfo : class, IAssetInfo, new()
    {
        public override Type InfoType { get; } = typeof(TInfo);

        public override void OnImportAsset(AssetImportContext context)
        {
            var info = AssetInfoEditorUtility.LoadInfo<TInfo>(context.assetPath);
            Object asset = OnCreateAsset(info);

            context.AddObjectToAsset("main", asset);
            context.SetMainObject(asset);
        }

        public override IAssetInfo Load()
        {
            var info = AssetInfoEditorUtility.LoadInfo<TInfo>(assetPath);

            return info;
        }

        public override void Save(IAssetInfo info)
        {
            AssetInfoEditorUtility.SaveInfo(assetPath, info);
        }

        protected abstract Object OnCreateAsset(TInfo info);
    }
}
