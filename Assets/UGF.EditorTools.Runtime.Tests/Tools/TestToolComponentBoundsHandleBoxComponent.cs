using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.Tools
{
    public class TestToolComponentBoundsHandleBoxComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 m_center;
        [SerializeField] private Vector3 m_size;

        public Vector3 Center { get { return m_center; } set { m_center = value; } }
        public Vector3 Size { get { return m_size; } set { m_size = value; } }
    }
}
