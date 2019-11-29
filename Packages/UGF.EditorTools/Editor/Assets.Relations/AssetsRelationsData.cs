using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets.Relations
{
    [Serializable]
    public class AssetsRelationsData
    {
        [SerializeField] private List<AssetsRelation> m_relations = new List<AssetsRelation>();

        public List<AssetsRelation> Relations { get { return m_relations; } }
    }
}
