using System;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.SettingsGroups;
using UGF.EditorTools.Runtime.IMGUI.SettingsGroups;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.SettingsGroups
{
    [CreateAssetMenu(menuName = "Tests/TestBuildSettingsAsset")]
    public class TestBuildSettingsAsset : ScriptableObject
    {
        [SerializeField] private TestBuildSettings m_settings = new TestBuildSettings();

        public TestBuildSettings Settings { get { return m_settings; } }
    }

    public interface ITestBuildSettings
    {
    }

    [Serializable]
    public class TestBuildSettings1 : ITestBuildSettings
    {
        [SerializeField] private bool m_bool;
        [SerializeField] private float m_float;
        [SerializeField] private Vector3 m_vector3;

        public bool Bool { get { return m_bool; } set { m_bool = value; } }
        public float Float { get { return m_float; } set { m_float = value; } }
        public Vector3 Vector3 { get { return m_vector3; } set { m_vector3 = value; } }
    }

    [Serializable]
    public class TestBuildSettings2 : ITestBuildSettings
    {
        [SerializeField] private ScriptableObject m_scriptableObject;
        [SerializeField] private Material m_material;
        [SerializeField] private LayerMask m_layerMask;

        public ScriptableObject ScriptableObject { get { return m_scriptableObject; } set { m_scriptableObject = value; } }
        public Material Material { get { return m_material; } set { m_material = value; } }
        public LayerMask LayerMask { get { return m_layerMask; } set { m_layerMask = value; } }
    }

    [Serializable]
    public class TestBuildSettings : SettingsGroups<ITestBuildSettings>
    {
    }

    [CustomPropertyDrawer(typeof(TestBuildSettings), true)]
    public class TestBuildSettingsDrawer : PropertyDrawerBase
    {
        private readonly SettingsGroupsDrawer m_drawer = new SettingsGroupsDrawer();

        public TestBuildSettingsDrawer()
        {
            string[] targets = Enum.GetNames(typeof(BuildTargetGroup));

            foreach (string target in targets)
            {
                m_drawer.Groups.Add(target);
                m_drawer.Toolbar.TabLabels.Add(new GUIContent(ObjectNames.NicifyVariableName(target)));
            }

            m_drawer.Toolbar.Count = m_drawer.Toolbar.TabLabels.Count;
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");

            m_drawer.DrawGUI(position, propertyGroups);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyGroups = serializedProperty.FindPropertyRelative("m_groups");


            return m_drawer.GetHeight(propertyGroups);
        }
    }
}
