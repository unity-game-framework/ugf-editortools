using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.EnabledProperty
{
    [CreateAssetMenu(menuName = "Tests/TestEnabledPropertyAsset")]
    public class TestEnabledPropertyAsset : ScriptableObject
    {
        [SerializeField] private EnabledProperty<bool> m_bool;
        [SerializeField] private EnabledProperty<float> m_float;
        [SerializeField] private EnabledProperty<Quaternion> m_quaternion;
        [SerializeField] private EnabledProperty<Rect> m_rect;
        [SerializeField] private EnabledProperty<Bounds> m_bounds;
        [SerializeField] private EnabledProperty<LayerMask> m_layerMask;
        [SerializeField] private EnabledProperty<Data> m_data;
        [SerializeField] private EnabledProperty<ScriptableObject> m_scriptableObject;
        [SerializeField] private List<EnabledProperty<Data>> m_list1 = new List<EnabledProperty<Data>>();
        [SerializeField] private List<EnabledProperty<ScriptableObject>> m_list2 = new List<EnabledProperty<ScriptableObject>>();

        public EnabledProperty<bool> Bool { get { return m_bool; } set { m_bool = value; } }
        public EnabledProperty<float> Float { get { return m_float; } set { m_float = value; } }
        public EnabledProperty<Quaternion> Quaternion { get { return m_quaternion; } set { m_quaternion = value; } }
        public EnabledProperty<Rect> Rect { get { return m_rect; } set { m_rect = value; } }
        public EnabledProperty<Bounds> Bounds { get { return m_bounds; } set { m_bounds = value; } }
        public EnabledProperty<LayerMask> LayerMask { get { return m_layerMask; } set { m_layerMask = value; } }
        public EnabledProperty<Data> DataProperty { get { return m_data; } set { m_data = value; } }
        public EnabledProperty<ScriptableObject> ScriptableObject { get { return m_scriptableObject; } set { m_scriptableObject = value; } }
        public List<EnabledProperty<Data>> List1 { get { return m_list1; } }
        public List<EnabledProperty<ScriptableObject>> List2 { get { return m_list2; } }

        [Serializable]
        public class Data
        {
            [SerializeField] private bool m_bool;
            [SerializeField] private float m_float;
            [SerializeField] private Quaternion m_quaternion;
            [SerializeField] private EnabledProperty<Vector3> m_vector3;

            public bool Bool { get { return m_bool; } set { m_bool = value; } }
            public float Float { get { return m_float; } set { m_float = value; } }
            public Quaternion Quaternion1 { get { return m_quaternion; } set { m_quaternion = value; } }
            public EnabledProperty<Vector3> Vector3 { get { return m_vector3; } set { m_vector3 = value; } }
        }
    }
}
