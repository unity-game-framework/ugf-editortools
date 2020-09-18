using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Dropdown
{
    [CreateAssetMenu(menuName = "Tests/DropdownTestAsset")]
    public class DropdownTestAsset : ScriptableObject
    {
        [SerializeField] private List<DataItem> m_items = new List<DataItem>();

        public List<DataItem> Items { get { return m_items; } }

        [Serializable]
        public class DataItem
        {
            [SerializeField, DropdownTest] private string m_value;
            [SerializeField, TypesDropdown] private string m_value2;
            [SerializeField] private bool m_bool;

            public string Value { get { return m_value; } set { m_value = value; } }
            public string Value2 { get { return m_value2; } set { m_value2 = value; } }
            public bool Bool { get { return m_bool; } set { m_bool = value; } }
        }
    }
}
