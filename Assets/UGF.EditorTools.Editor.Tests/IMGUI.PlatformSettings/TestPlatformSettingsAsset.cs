using System;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.PlatformSettings
{
    [CreateAssetMenu(menuName = "Tests/TestPlatformSettingsAsset")]
    public class TestPlatformSettingsAsset : ScriptableObject
    {
        [SerializeField] private PlatformSettings<ITestPlatformSettings> m_settings = new PlatformSettings<ITestPlatformSettings>();
        [SerializeField] private PlatformSettings<object> m_settings2 = new PlatformSettings<object>();

        public PlatformSettings<ITestPlatformSettings> Settings { get { return m_settings; } }
    }

    public interface ITestPlatformSettings
    {
    }

    [Serializable]
    public class TestPlatformSettingsA : ITestPlatformSettings
    {
        [SerializeField] private bool m_bool;
        [SerializeField] private float m_float;
        [SerializeField] private Vector3 m_vector3;

        public bool Bool { get { return m_bool; } set { m_bool = value; } }
        public float Float { get { return m_float; } set { m_float = value; } }
        public Vector3 Vector3 { get { return m_vector3; } set { m_vector3 = value; } }
    }

    [Serializable]
    public class TestPlatformSettingsB : ITestPlatformSettings
    {
        [SerializeField] private ScriptableObject m_scriptableObject;
        [SerializeField] private Material m_material;
        [SerializeField] private LayerMask m_layerMask;

        public ScriptableObject ScriptableObject { get { return m_scriptableObject; } set { m_scriptableObject = value; } }
        public Material Material { get { return m_material; } set { m_material = value; } }
        public LayerMask LayerMask { get { return m_layerMask; } set { m_layerMask = value; } }
    }

    [CustomPropertyDrawer(typeof(PlatformSettings<ITestPlatformSettings>), true)]
    public class TestPlatformSettingsDrawer : PlatformSettingsPropertyDrawer
    {
        public TestPlatformSettingsDrawer()
        {
            Drawer.AutoSettingsInstanceCreation = true;
            Drawer.AllowEmptySettings = false;
            Drawer.AddGroupType(BuildTargetGroup.Standalone.ToString(), typeof(TestPlatformSettingsA));
            Drawer.AddGroupType(BuildTargetGroup.Android.ToString(), typeof(TestPlatformSettingsB));
        }
    }

    [CustomPropertyDrawer(typeof(PlatformSettings<object>), true)]
    public class TestPlatformSettingsDrawer2 : PlatformSettingsPropertyDrawer
    {
        public TestPlatformSettingsDrawer2()
        {
            Drawer.ClearGroups();
            Drawer.AddPlatformAll();
        }
    }
}
