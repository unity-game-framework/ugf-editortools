using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets.Relations
{
    [Serializable]
    public class AssetsRelation
    {
        [SerializeField] private string m_parent;
        [SerializeField] private List<string> m_children;

        public string Parent { get { return m_parent; } set { m_parent = value; } }
        public List<string> Children { get { return m_children; } }

        public AssetsRelation(string parent, IEnumerable<string> children)
        {
            if (string.IsNullOrEmpty(parent)) throw new ArgumentException("Value cannot be null or empty.", nameof(parent));
            if (children == null) throw new ArgumentNullException(nameof(children));

            m_parent = parent;
            m_children = new List<string>(children);
        }
    }
}
