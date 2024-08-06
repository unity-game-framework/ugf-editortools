using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Assets
{
    [CreateAssetMenu(menuName = "Tests/TestAssetIdReferenceAsset2")]
    public class TestAssetIdReferenceAsset2 : ScriptableObject
    {
        [SerializeField] private ScriptableObject m_test;
        [SerializeField] private AssetIdReference<ScriptableObject> m_scriptable;
        [SerializeField] private AssetIdReference<Material> m_material;
        [SerializeField] private List<AssetIdReference<Material>> m_list = new List<AssetIdReference<Material>>();

        public ScriptableObject Test { get { return m_test; } set { m_test = value; } }
        public AssetIdReference<ScriptableObject> Scriptable { get { return m_scriptable; } set { m_scriptable = value; } }
        public AssetIdReference<Material> Material { get { return m_material; } set { m_material = value; } }
        public List<AssetIdReference<Material>> List { get { return m_list; } }
    }
}
