using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.Tools
{
    public class TestToolComponentHandlePositionComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 m_position;

        public Vector3 Position { get { return m_position; } set { m_position = value; } }
    }
}
