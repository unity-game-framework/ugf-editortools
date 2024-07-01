using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Runtime.IMGUI.SettingsGroups
{
    [Serializable]
    public class SettingsGroups<TSettings> where TSettings : class
    {
        [SerializeField] private List<SettingsGroup<TSettings>> m_groups = new List<SettingsGroup<TSettings>>();

        public List<SettingsGroup<TSettings>> Groups { get { return m_groups; } }

        public bool HasSettings(string name)
        {
            return TryGetGroup(name, out SettingsGroup<TSettings> group) && group.HasSettings;
        }

        public bool HasGroup(string name)
        {
            return TryGetGroup(name, out _);
        }

        public void SetSettings(string name, TSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (!TryGetGroup(name, out SettingsGroup<TSettings> group))
            {
                group = new SettingsGroup<TSettings>
                {
                    Name = name
                };
            }

            group.Settings = settings;
        }

        public bool TrySetSettings(string name, TSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (TryGetGroup(name, out SettingsGroup<TSettings> group))
            {
                group.Settings = settings;
                return true;
            }

            return false;
        }

        public T GetSettings<T>(string name) where T : class
        {
            return TryGetSettings(name, out T settings) ? settings : throw new ArgumentException($"Settings not found by the specified name: '{name}'.");
        }

        public bool TryGetSettings<T>(string name, out T settings) where T : class
        {
            settings = default;
            return TryGetGroup(name, out SettingsGroup<TSettings> group) && group.TryGetSettings(out settings);
        }

        public bool TryClearSettings(string name)
        {
            if (TryGetGroup(name, out SettingsGroup<TSettings> group))
            {
                group.Settings = default;
                return true;
            }

            return false;
        }

        public SettingsGroup<TSettings> GetGroup(string name)
        {
            return TryGetGroup(name, out SettingsGroup<TSettings> group) ? group : throw new ArgumentException($"Group not found by the specified name: '{name}'.");
        }

        public bool TryGetGroup(string name, out SettingsGroup<TSettings> group)
        {
            for (int i = 0; i < m_groups.Count; i++)
            {
                group = m_groups[i];

                if (group.Name == name)
                {
                    return true;
                }
            }

            group = default;
            return false;
        }
    }
}
