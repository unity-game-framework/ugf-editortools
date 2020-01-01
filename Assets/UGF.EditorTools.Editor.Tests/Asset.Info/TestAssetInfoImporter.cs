using UGF.EditorTools.Editor.Asset.Info;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Asset.Info
{
    [ScriptedImporter(0, "info")]
    public class TestAssetInfoImporter : AssetInfoTextImporter<TestAssetInfo>
    {
        [SerializeField] private int m_intValue = 15;

        public int IntValue { get { return m_intValue; } set { m_intValue = value; } }

        protected override string OnCreateTextAsset(TestAssetInfo info)
        {
            string text = EditorJsonUtility.ToJson(info);

            return text;
        }
    }
}
