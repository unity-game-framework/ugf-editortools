using System;
using UGF.EditorTools.Editor.Asset.Processor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Asset.Processor
{
    [Serializable]
    public class TestAssetProcessor : AssetProcessor
    {
        [SerializeField] private string m_name = "Test";

        public string Name { get { return m_name; } set { m_name = value; } }
    }
}
