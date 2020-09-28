using System;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Platforms
{
    [Serializable]
    public class PlatformGroup<TTarget>
    {
        [SerializeField] private TTarget m_target;
        [SerializeReference, ManagedReference] private IPlatformSettings m_settings;

        public TTarget Target { get { return m_target; } set { m_target = value; } }
        public IPlatformSettings Settings { get { return m_settings ?? throw new ArgumentException("No settings specified."); } set { m_settings = value; } }
        public bool HasSettings { get { return m_settings != null; } }

        public T GetSettings<T>() where T : IPlatformSettings
        {
            return TryGetSettings(out T settings) ? settings : throw new ArgumentException($"No settings with specified type: '{typeof(T)}'.");
        }

        public bool TryGetSettings<T>(out T settings) where T : IPlatformSettings
        {
            if (m_settings is T value)
            {
                settings = value;
                return true;
            }

            settings = default;
            return false;
        }
    }
}
