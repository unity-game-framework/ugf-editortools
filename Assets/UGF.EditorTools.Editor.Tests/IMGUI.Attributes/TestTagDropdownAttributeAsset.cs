using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Attributes
{
    [CreateAssetMenu(menuName = "Tests/TestTagDropdownAttributeAsset")]
    public class TestTagDropdownAttributeAsset : ScriptableObject
    {
        [SerializeField, TagDropdown] private string m_tag;

        public string Tag { get { return m_tag; } set { m_tag = value; } }
    }
}
