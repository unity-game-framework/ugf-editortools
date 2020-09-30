using System;
using UGF.EditorTools.Runtime.IMGUI.SettingsGroups;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.SettingsGroups
{
    [CreateAssetMenu(menuName = "Tests/TestSettingsGroupsAsset")]
    public class TestSettingsGroupsAsset : ScriptableObject
    {
        [SerializeField] private SettingsGroups<ITestSettings> m_settings = new SettingsGroups<ITestSettings>();

        public SettingsGroups<ITestSettings> Settings { get { return m_settings; } }
    }

    public interface ITestSettings
    {
    }

    [Serializable]
    public class TestSettings1 : ITestSettings
    {
        [SerializeField] private bool m_bool;
        [SerializeField] private float m_float;
        [SerializeField] private Vector3 m_vector3;

        public bool Bool { get { return m_bool; } set { m_bool = value; } }
        public float Float { get { return m_float; } set { m_float = value; } }
        public Vector3 Vector3 { get { return m_vector3; } set { m_vector3 = value; } }
    }

    [Serializable]
    public class TestSettings2 : ITestSettings
    {
        [SerializeField] private ScriptableObject m_scriptableObject;
        [SerializeField] private Material m_material;
        [SerializeField] private LayerMask m_layerMask;

        public ScriptableObject ScriptableObject { get { return m_scriptableObject; } set { m_scriptableObject = value; } }
        public Material Material { get { return m_material; } set { m_material = value; } }
        public LayerMask LayerMask { get { return m_layerMask; } set { m_layerMask = value; } }
    }
}
