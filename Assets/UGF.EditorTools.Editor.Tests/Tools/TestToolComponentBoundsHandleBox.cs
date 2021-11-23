using UGF.EditorTools.Editor.Tools;
using UGF.EditorTools.Runtime.Tests;
using UGF.EditorTools.Runtime.Tests.Tools;
using UnityEditor.EditorTools;

namespace UGF.EditorTools.Editor.Tests.Tools
{
    [EditorTool("TestToolComponentBoundsHandleBox", typeof(TestToolComponentBoundsHandleBoxComponent))]
    public class TestToolComponentBoundsHandleBox : ToolComponentBoundsHandleBox
    {
        public TestToolComponentBoundsHandleBox() : base("m_center", "m_size")
        {
        }
    }
}
