using System.Collections.Generic;
using UGF.EditorTools.Runtime.ComponentReferences;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.ComponentReferences
{
    [AddComponentMenu("Tests/TestComponentReferenceComponent")]
    public class TestComponentReferenceComponent : MonoBehaviour
    {
        [SerializeField] private ComponentReference<Transform> m_reference;
        [SerializeField] private ComponentReference<Component> m_reference1;
        [SerializeField] private List<ComponentReference<Transform>> m_list = new List<ComponentReference<Transform>>();
        [SerializeField] private List<ComponentReference<Camera>> m_list2 = new List<ComponentReference<Camera>>();

        public ComponentReference<Transform> Reference { get { return m_reference; } set { m_reference = value; } }
        public ComponentReference<Component> Reference1 { get { return m_reference1; } set { m_reference1 = value; } }
        public List<ComponentReference<Transform>> List { get { return m_list; } }
        public List<ComponentReference<Camera>> List2 { get { return m_list2; } }
    }
}
