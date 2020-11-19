using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Types
{
    [CreateAssetMenu(menuName = "Tests/TestTypeReferenceAsset")]
    public class TestTypeReferenceAsset : ScriptableObject
    {
        [SerializeField] private TypeReference m_type;

        public TypeReference Type { get { return m_type; } set { m_type = value; } }
    }
}
