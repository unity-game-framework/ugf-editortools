using System;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Platforms
{
    [Serializable]
    public class PlatformGroup<TTarget> : IPlatformGroup
    {
        [SerializeField] private TTarget m_target;
        [SerializeReference, ManagedReference] private IPlatformSettings m_settings;

        public TTarget Target { get { return m_target; } set { m_target = value; } }
        public IPlatformSettings Settings { get { return m_settings; } set { m_settings = value; } }

        object IPlatformGroup.Target { get { return m_target; } }
    }
}
