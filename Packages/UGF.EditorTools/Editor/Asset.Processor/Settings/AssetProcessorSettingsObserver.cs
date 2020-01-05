using System;
using System.Collections.Generic;

namespace UGF.EditorTools.Editor.Asset.Processor.Settings
{
    internal class AssetProcessorSettingsObserver
    {
        public AssetProcessorProvider Provider { get; }
        public AssetProcessorSettingsData Data { get; }

        public AssetProcessorSettingsObserver(AssetProcessorProvider provider, AssetProcessorSettingsData data)
        {
            Provider = provider;
            Data = data;
        }

        public void UpdateProvider()
        {
            Provider.Clear();

            for (int i = 0; i < Data.Assets.Count; i++)
            {
                AssetProcessorSettingsData.AssetInfo asset = Data.Assets[i];

                foreach (IAssetProcessor processor in asset.Processors)
                {
                    Provider.Add(asset.Guid, processor);
                }
            }
        }

        public void UpdateData()
        {
            Data.Assets.Clear();

            foreach (KeyValuePair<string, AssetProcessorProvider.ProcessorCollectionEnumerable> processors in Provider)
            {
                var asset = new AssetProcessorSettingsData.AssetInfo
                {
                    Guid = processors.Key
                };

                foreach (KeyValuePair<Type, IAssetProcessor> pair in processors.Value)
                {
                    asset.Processors.Add(pair.Value);
                }
            }
        }
    }
}
