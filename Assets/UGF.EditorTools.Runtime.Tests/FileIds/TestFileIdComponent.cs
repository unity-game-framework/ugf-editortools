using UGF.EditorTools.Runtime.FileIds;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.EditorTools.Runtime.Tests.FileIds
{
    public class TestFileIdComponent : MonoBehaviour
    {
        [FileId(typeof(MeshFilter))]
        [SerializeField] private FileId m_fileId;
        [FileId(typeof(MeshRenderer))]
        [SerializeField] private FileId m_fileId2;

        public FileId FileId { get { return m_fileId; } set { m_fileId = value; } }
        public FileId FileId2 { get { return m_fileId2; } set { m_fileId2 = value; } }
    }
}
