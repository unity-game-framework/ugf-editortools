using System;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.References
{
    [CreateAssetMenu(menuName = "Tests/ManagedReferenceSelectTestAsset")]
    public class ManagedReferenceSelectTestAsset : ScriptableObject
    {
        [SerializeReference, ManagedReference(typeof(IManagedReferenceTest))]
        private IManagedReferenceTest m_test;
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
