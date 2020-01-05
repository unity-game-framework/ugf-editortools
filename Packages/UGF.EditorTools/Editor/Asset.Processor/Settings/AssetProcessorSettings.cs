using System;
using System.Collections.Generic;
using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.EditorTools.Editor.Asset.Processor.Settings
{
    [InitializeOnLoad]
    public static class AssetProcessorSettings
    {
        public static bool Active
        {
            get { return m_settings.Data.Active; }
            set
            {
                m_settings.Data.Active = true;
                m_settings.SaveSettings();
            }
        }

        private static readonly CustomSettingsEditorPackage<AssetProcessorSettingsData> m_settings = new CustomSettingsEditorPackage<AssetProcessorSettingsData>
        (
            "UGF.EditorTools",
            "AssetProcessorSettings",
            CustomSettingsEditorUtility.DEFAULT_PACKAGE_EXTERNAL_FOLDER
        );

        private static readonly AssetProcessorProvider m_provider = new AssetProcessorProvider();
        private static readonly AssetProcessorSettingsObserver m_observer = new AssetProcessorSettingsObserver(m_provider, m_settings.Data);

        static AssetProcessorSettings()
        {
            m_settings.Saved += OnDataChanged;
            m_settings.Loaded += OnDataChanged;
        }

        public static void Add(string guid, IAssetProcessor processor)
        {
            m_provider.Add(guid, processor);
            m_observer.UpdateData();
            m_settings.SaveSettings();
        }

        public static void Remove(string guid)
        {
            m_provider.Remove(guid);
            m_observer.UpdateData();
            m_settings.SaveSettings();
        }

        public static void Remove(string guid, Type processorType)
        {
            m_provider.Remove(guid, processorType);
            m_observer.UpdateData();
            m_settings.SaveSettings();
        }

        public static void Clear()
        {
            m_provider.Clear();
            m_observer.UpdateData();
            m_settings.SaveSettings();
        }

        public static bool TryGet<T>(string guid, Type processorType, out T processor) where T : IAssetProcessor
        {
            return m_provider.TryGet(guid, processorType, out processor);
        }

        public static bool TryGet(string guid, Type processorType, out IAssetProcessor processor)
        {
            return m_provider.TryGet(guid, processorType, out processor);
        }

        public static bool TryGetAll(string guid, out IReadOnlyDictionary<Type, IAssetProcessor> processors)
        {
            return m_provider.TryGetAll(guid, out processors);
        }

        public static bool TryGetAllOrdered(string guid, out IReadOnlyList<IAssetProcessor> processors)
        {
            return m_provider.TryGetAllOrdered(guid, out processors);
        }

        private static void OnDataChanged()
        {
            m_observer.UpdateProvider();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<AssetProcessorSettingsData>("Project/UGF/Asset Processors", m_settings, SettingsScope.Project);
        }
    }
}
