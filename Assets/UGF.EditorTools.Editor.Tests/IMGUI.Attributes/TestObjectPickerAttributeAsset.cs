using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Attributes
{
    [CreateAssetMenu(menuName = "Tests/TestObjectPickerAttributeAsset")]
    public class TestObjectPickerAttributeAsset : ScriptableObject
    {
        [ObjectPicker]
        [SerializeField] private Object m_target1;
        [ObjectPicker(typeof(ScriptableObject))]
        [SerializeField] private Object m_target2;
        [ObjectPicker("t:folder")]
        [SerializeField] private Object m_target3;
        [ObjectPicker(typeof(DefaultAsset), "t:folder")]
        [SerializeField] private Object m_target4;

        public Object Target1 { get { return m_target1; } set { m_target1 = value; } }
        public Object Target2 { get { return m_target2; } set { m_target2 = value; } }
        public Object Target3 { get { return m_target3; } set { m_target3 = value; } }
        public Object Target4 { get { return m_target4; } set { m_target4 = value; } }
    }
}
