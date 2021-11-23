using UGF.EditorTools.Editor.Tools;
using UGF.EditorTools.Runtime.Tests.Tools;
using UnityEditor.EditorTools;

namespace UGF.EditorTools.Editor.Tests.Tools
{
    [EditorTool("TestToolComponentHandlePosition", typeof(TestToolComponentHandlePositionComponent))]
    public class TestToolComponentHandlePosition : ToolComponentHandlePosition
    {
        public TestToolComponentHandlePosition() : base("m_position")
        {
        }
    }
}
