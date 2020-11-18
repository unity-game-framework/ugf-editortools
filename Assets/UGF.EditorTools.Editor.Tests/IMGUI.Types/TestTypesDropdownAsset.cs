using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Types
{
    [CreateAssetMenu(menuName = "Tests/TestTypesDropdownAsset")]
    public class TestTypesDropdownAsset : ScriptableObject
    {
        [TypesDropdown]
        [SerializeField] private string m_type;

        public string Type { get { return m_type; } set { m_type = value; } }
    }
}
