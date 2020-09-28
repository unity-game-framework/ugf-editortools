using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Platforms
{
    [Serializable]
    public class PlatformSettings<TGroup> where TGroup : IPlatformGroup
    {
        [SerializeField] private List<TGroup> m_groups = new List<TGroup>();

        public List<TGroup> Groups { get { return m_groups; } }
    }
}
