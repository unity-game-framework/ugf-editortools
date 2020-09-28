using System;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Platforms
{
    [Serializable]
    public class PlatformGroup
    {
        [SerializeField] private BuildTargetGroup m_target;
        [SerializeReference, ManagedReference] private IPlatformSettings m_settings;

        public BuildTargetGroup Target { get { return m_target; } set { m_target = value; } }
        public IPlatformSettings Settings { get { return m_settings; } set { m_settings = value; } }
    }
}
