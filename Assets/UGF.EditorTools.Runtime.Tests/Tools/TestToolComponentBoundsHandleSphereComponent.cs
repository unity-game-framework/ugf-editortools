using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.Tools
{
    public class TestToolComponentBoundsHandleSphereComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 m_center;
        [SerializeField] private float m_radius;

        public Vector3 Center { get { return m_center; } set { m_center = value; } }
        public float Radius { get { return m_radius; } set { m_radius = value; } }
    }
}
