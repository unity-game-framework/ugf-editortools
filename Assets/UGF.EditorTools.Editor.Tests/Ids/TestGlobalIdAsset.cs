using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Ids
{
    [CreateAssetMenu(menuName = "Tests/TestGlobalIdAsset")]
    public class TestGlobalIdAsset : ScriptableObject
    {
        [SerializeField] private GlobalId m_id;

        public GlobalId Id { get { return m_id; } set { m_id = value; } }
    }
}
