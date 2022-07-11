using UGF.EditorTools.Runtime.FileIds;
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
        [SerializeField] private FileId m_fileId5;
        [FileId(typeof(Camera))]
        [SerializeField] private FileId m_fileId6;

        public string FileId { get { return m_fileId; } set { m_fileId = value; } }
        public string FileId1 { get { return m_fileId1; } set { m_fileId1 = value; } }
        public string FileId2 { get { return m_fileId2; } set { m_fileId2 = value; } }
        public string FileId3 { get { return m_fileId3; } set { m_fileId3 = value; } }
        public string FileId4 { get { return m_fileId4; } set { m_fileId4 = value; } }
        public FileId FileId5 { get { return m_fileId5; } set { m_fileId5 = value; } }
        public FileId FileId6 { get { return m_fileId6; } set { m_fileId6 = value; } }
    }
}
