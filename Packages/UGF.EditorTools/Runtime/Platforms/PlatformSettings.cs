using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Platforms
{
    [Serializable]
    public class PlatformSettings<TTarget>
    {
        [SerializeField] private List<PlatformGroup<TTarget>> m_groups = new List<PlatformGroup<TTarget>>();

        public List<PlatformGroup<TTarget>> Groups { get { return m_groups; } }

        public bool HasSettings(TTarget target, IEqualityComparer<TTarget> comparer = null)
        {
            return TryGetGroup(target, out PlatformGroup<TTarget> group, comparer) && group.HasSettings;
        }

        public bool HasGroup(TTarget target, IEqualityComparer<TTarget> comparer = null)
        {
            return TryGetGroup(target, out _, comparer);
        }

        public void SetSettings(TTarget target, IPlatformSettings settings, IEqualityComparer<TTarget> comparer = null)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (!TryGetGroup(target, out PlatformGroup<TTarget> group, comparer))
            {
                group = new PlatformGroup<TTarget>
                {
                    Target = target
                };
            }

            group.Settings = settings;
        }

        public bool TrySetSettings(TTarget target, IPlatformSettings settings, IEqualityComparer<TTarget> comparer = null)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            if (TryGetGroup(target, out PlatformGroup<TTarget> group, comparer))
            {
                group.Settings = settings;
                return true;
            }

            return false;
        }

        public T GetSettings<T>(TTarget target, IEqualityComparer<TTarget> comparer = null) where T : IPlatformSettings
        {
            return TryGetSettings(target, out T settings, comparer) ? settings : throw new ArgumentException($"Settings not found by the specified target: '{target}'.");
        }

        public bool TryGetSettings<T>(TTarget target, out T settings, IEqualityComparer<TTarget> comparer = null) where T : IPlatformSettings
        {
            settings = default;
            return TryGetGroup(target, out PlatformGroup<TTarget> group, comparer) && group.TryGetSettings(out settings);
        }

        public bool TryClearSettings(TTarget target, IEqualityComparer<TTarget> comparer = null)
        {
            if (TryGetGroup(target, out PlatformGroup<TTarget> group, comparer))
            {
                group.Settings = null;
            }

            return false;
        }

        public PlatformGroup<TTarget> GetGroup(TTarget target, IEqualityComparer<TTarget> comparer = null)
        {
            return TryGetGroup(target, out PlatformGroup<TTarget> group, comparer) ? group : throw new ArgumentException($"Group not found by the specified target: '{target}'.");
        }

        public bool TryGetGroup(TTarget target, out PlatformGroup<TTarget> group, IEqualityComparer<TTarget> comparer = null)
        {
            if (comparer == null) comparer = EqualityComparer<TTarget>.Default;

            for (int i = 0; i < m_groups.Count; i++)
            {
                group = m_groups[i];

                if (comparer.Equals(group.Target, target))
                {
                    return true;
                }
            }

            group = default;
            return false;
        }
    }
}
