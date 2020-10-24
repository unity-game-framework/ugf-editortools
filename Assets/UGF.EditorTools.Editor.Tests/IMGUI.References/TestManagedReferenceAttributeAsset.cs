using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.References
{
    [CreateAssetMenu(menuName = "Tests/TestManagedReferenceAttributeAsset")]
    public class TestManagedReferenceAttributeAsset : ScriptableObject
    {
        [SerializeReference, ManagedReference] private IManagedReferenceTest m_test;

        public IManagedReferenceTest Test { get { return m_test; } set { m_test = value; } }
    }
}
