using System;
using UGF.EditorTools.Editor.IMGUI.Platforms;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Platforms
{
    [Serializable]
    public class TestPlatformSettings1 : IPlatformSettings
    {
        [SerializeField] private bool m_bool;
        [SerializeField] private float m_float;
        [SerializeField] private Vector3 m_vector3;

        public bool Bool { get { return m_bool; } set { m_bool = value; } }
        public float Float { get { return m_float; } set { m_float = value; } }
        public Vector3 Vector3 { get { return m_vector3; } set { m_vector3 = value; } }
    }
}
