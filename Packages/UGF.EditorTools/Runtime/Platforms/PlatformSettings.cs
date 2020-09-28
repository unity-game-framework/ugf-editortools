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
    }
}
