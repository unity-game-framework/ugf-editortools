using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Assets
{
    [CreateAssetMenu(menuName = "Tests/TestAssetIdAsset")]
    public class TestAssetIdAsset : ScriptableObject
    {
        [AssetId]
        [SerializeField] private Hash128 m_id;
        [AssetId(typeof(Material))]
        [SerializeField] private Hash128 m_material;
        [AssetId(typeof(Material))]
        [SerializeField] private Hash128 m_material2;
        [AssetId(typeof(Material))]
        [SerializeField] private Hash128 m_material3;

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
        public GlobalId Material { get { return m_material; } set { m_material = value; } }
        public GlobalId Material2 { get { return m_material2; } set { m_material2 = value; } }
        public Hash128 Material3 { get { return m_material3; } set { m_material3 = value; } }
    }
}
