using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Types
{
    [CreateAssetMenu(menuName = "Tests/TestTypeReferenceAsset")]
    public class TestTypeReferenceAsset : ScriptableObject
    {
        [TypeReferenceDropdown]
        [SerializeField] private TypeReference<object> m_type;
        [TypeReferenceDropdown(typeof(ScriptableObject))]
        [SerializeField] private TypeReference<object> m_type2;
        [TypeReferenceDropdown(typeof(ScriptableObject))]
        [SerializeField] private TypeReference m_type3;

        public TypeReference<object> Type { get { return m_type; } set { m_type = value; } }
        public TypeReference<object> Type2 { get { return m_type2; } set { m_type2 = value; } }
        public TypeReference Type3 { get { return m_type3; } set { m_type3 = value; } }
    }
}
