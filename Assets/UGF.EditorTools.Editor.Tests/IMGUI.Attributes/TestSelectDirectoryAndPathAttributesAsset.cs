using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Attributes
{
    [CreateAssetMenu(menuName = "Tests/TestSelectDirectoryAndPathAttributesAsset")]
    public class TestSelectDirectoryAndPathAttributesAsset : ScriptableObject
    {
        [SelectDirectory]
        [SerializeField] private string m_directory;
        [SelectFile]
        [SerializeField] private string m_file;

        public string Directory { get { return m_directory; } set { m_directory = value; } }
        public string File { get { return m_file; } set { m_file = value; } }
    }
}
