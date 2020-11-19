using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Types
{
    [CreateAssetMenu(menuName = "Tests/TestTypesDropdownNestedAsset")]
    public class TestTypesDropdownNestedAsset : ScriptableObject
    {
        [TypesDropdown(typeof(ITestTypesDropdownNested), DisplayFullPath = false)]
        [SerializeField] private string m_type;

        public string Type { get { return m_type; } set { m_type = value; } }
    }

    public interface ITestTypesDropdownNested
    {
    }

    public class TestTypesDropdownNestedParentA : ITestTypesDropdownNested
    {
        public class NestedA : ITestTypesDropdownNested
        {
        }

        public class NestedB : ITestTypesDropdownNested
        {
        }
    }

    public class TestTypesDropdownNestedParentB : ITestTypesDropdownNested
    {
        public class NestedA : ITestTypesDropdownNested
        {
        }

        public class NestedB : ITestTypesDropdownNested
        {
        }
    }

    public class TestTypesDropdownNestedParentC : ITestTypesDropdownNested
    {
        public class A : ITestTypesDropdownNested
        {
            public class B : ITestTypesDropdownNested
            {
                public class C : ITestTypesDropdownNested
                {
                }
            }
        }
    }
}
