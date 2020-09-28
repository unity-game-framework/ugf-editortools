using System;
using UGF.EditorTools.Runtime.Platforms;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Platforms
{
    [Serializable]
    public class TestPlatformSettings2 : IPlatformSettings
    {
        [SerializeField] private ScriptableObject m_scriptableObject;
        [SerializeField] private Material m_material;
        [SerializeField] private LayerMask m_layerMask;

        public ScriptableObject ScriptableObject { get { return m_scriptableObject; } set { m_scriptableObject = value; } }
        public Material Material { get { return m_material; } set { m_material = value; } }
        public LayerMask LayerMask { get { return m_layerMask; } set { m_layerMask = value; } }
    }
}
