﻿using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Ids
{
    [CreateAssetMenu(menuName = "Tests/TestGlobalIdAsset")]
    public class TestGlobalIdAsset : ScriptableObject
    {
        [SerializeField] private GlobalId m_id;
        [AssetId(typeof(Material))]
        [SerializeField] private GlobalId m_id2;

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
        public GlobalId ID2 { get { return m_id2; } set { m_id2 = value; } }
    }
}
