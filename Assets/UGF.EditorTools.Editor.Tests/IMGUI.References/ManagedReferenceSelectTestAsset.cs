using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.References
{
    [CreateAssetMenu(menuName = "Tests/ManagedReferenceSelectTestAsset")]
    public class ManagedReferenceSelectTestAsset : ScriptableObject
    {
        [SerializeReference, ManagedReference] private IManagedReferenceTest m_test;

        [SerializeReference, ManagedReference(typeof(object))]
        private object m_test2;

        [SerializeReference, ManagedReference(typeof(IManagedReferenceTest))]
        private List<IManagedReferenceTest> m_test3;

        [SerializeReference, ManagedReference(typeof(IManagedReferenceTest))]
        private IManagedReferenceTest[] m_test4;

        [SerializeReference, ManagedReference] private IManagedReferenceTest[] m_test5;
        [SerializeReference, ManagedReference] private List<IManagedReferenceTest> m_test6;

        [SerializeReference, ManagedReference(typeof(ManagedReferenceTest))]
        private IManagedReferenceTest m_test7;
    }

    public interface IManagedReferenceTest
    {
    }

    [Serializable]
    public class ManagedReferenceTest : IManagedReferenceTest
    {
        [SerializeField] private bool m_bool;

        public bool B { get { return m_bool; } set { m_bool = value; } }
    }

    [Serializable]
    public class ManagedReferenceTest2 : IManagedReferenceTest
    {
        [SerializeField] private int m_int;

        public int I { get { return m_int; } set { m_int = value; } }
    }
}
