using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Types
{
    [CreateAssetMenu(menuName = "Tests/TestAssemblyReferenceAsset")]
    public class TestAssemblyReferenceAsset : ScriptableObject
    {
        [AssemblyReferenceDropdown]
        [SerializeField] private AssemblyReference m_assembly;

        public AssemblyReference Assembly { get { return m_assembly; } set { m_assembly = value; } }
    }
}
