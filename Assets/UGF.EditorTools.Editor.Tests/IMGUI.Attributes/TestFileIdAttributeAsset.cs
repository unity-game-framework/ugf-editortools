using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Attributes
{
    [CreateAssetMenu(menuName = "Tests/TestFileIdAttributeAsset")]
    public class TestFileIdAttributeAsset : ScriptableObject
    {
        [FileId]
        [SerializeField] private string m_fileId;
        [FileId(typeof(Transform))]
        [SerializeField] private string m_fileId1;
        [FileId(typeof(Light))]
        [SerializeField] private string m_fileId2;
        [FileId]
        [SerializeField] private string m_fileId3;
        [FileId(typeof(Camera))]
        [SerializeField] private string m_fileId4;

        public string FileId { get { return m_fileId; } set { m_fileId = value; } }
        public string FileId1 { get { return m_fileId1; } set { m_fileId1 = value; } }
        public string FileId2 { get { return m_fileId2; } set { m_fileId2 = value; } }
        public string FileId3 { get { return m_fileId3; } set { m_fileId3 = value; } }
        public string FileId4 { get { return m_fileId4; } set { m_fileId4 = value; } }
    }
}
