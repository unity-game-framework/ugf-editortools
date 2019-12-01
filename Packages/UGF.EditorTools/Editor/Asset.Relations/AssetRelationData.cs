using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.Asset.Relations
{
    [Serializable]
    public class AssetRelationData
    {
        [SerializeField] private List<AssetRelation> m_relations = new List<AssetRelation>();

        public List<AssetRelation> Relations { get { return m_relations; } }
    }
}
