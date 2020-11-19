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

        public TypeReference<object> Type { get { return m_type; } set { m_type = value; } }
        public TypeReference<object> Type2 { get { return m_type2; } set { m_type2 = value; } }
    }
}
