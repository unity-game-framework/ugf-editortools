﻿using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Assets
{
    [CreateAssetMenu(menuName = "Tests/TestAssetIdAsset")]
    public class TestAssetIdAsset : ScriptableObject
    {
        [AssetId]
        [SerializeField] private GlobalId m_id;
        [AssetId(typeof(Material))]
        [SerializeField] private GlobalId m_material;
        [AssetId(typeof(Material))]
        [SerializeField] private GlobalId m_material2;

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
        public GlobalId Material { get { return m_material; } set { m_material = value; } }
        public GlobalId Material2 { get { return m_material2; } set { m_material2 = value; } }
    }
}
