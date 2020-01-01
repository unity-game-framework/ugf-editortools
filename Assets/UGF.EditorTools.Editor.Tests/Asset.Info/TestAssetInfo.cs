using System;
using UGF.EditorTools.Editor.Asset.Info;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Asset.Info
{
    [Serializable]
    public class TestAssetInfo : IAssetInfo
    {
        [SerializeField] private string m_name = "Test";
        [SerializeField] private bool m_boolValue = true;

        public string Name { get { return m_name; } set { m_name = value; } }
        public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
    }
}
