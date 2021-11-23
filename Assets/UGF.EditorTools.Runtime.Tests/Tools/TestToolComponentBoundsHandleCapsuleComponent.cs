using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.Tools
{
    public class TestToolComponentBoundsHandleCapsuleComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 m_center;
        [SerializeField] private float m_height;
        [SerializeField] private float m_radius;

        public Vector3 Center { get { return m_center; } set { m_center = value; } }
        public float Height { get { return m_height; } set { m_height = value; } }
        public float Radius { get { return m_radius; } set { m_radius = value; } }
    }
}
