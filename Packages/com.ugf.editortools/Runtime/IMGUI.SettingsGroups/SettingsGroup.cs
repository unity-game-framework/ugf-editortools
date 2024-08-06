using System;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.SettingsGroups
{
    [Serializable]
    public class SettingsGroup<TSettings> where TSettings : class
    {
        [SerializeField] private string m_name;
        [SerializeReference, ManagedReference] private TSettings m_settings;

        public string Name { get { return m_name; } set { m_name = value; } }
        public TSettings Settings { get { return m_settings ?? throw new ArgumentException("No settings specified."); } set { m_settings = value; } }
        public bool HasSettings { get { return m_settings != null; } }

        public T GetSettings<T>() where T : class
        {
            return TryGetSettings(out T settings) ? settings : throw new ArgumentException($"No settings with specified type: '{typeof(T)}'.");
        }

        public bool TryGetSettings<T>(out T settings) where T : class
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
