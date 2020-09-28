using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Platforms
{
    [Serializable]
    public class PlatformSettings
    {
        [SerializeField] private List<PlatformGroup> m_groups = new List<PlatformGroup>();

        public List<PlatformGroup> Groups { get { return m_groups; } }
    }
}
