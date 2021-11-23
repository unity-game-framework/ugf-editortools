using UGF.EditorTools.Editor.Tools;
using UGF.EditorTools.Runtime.Tests.Tools;
using UnityEditor.EditorTools;

namespace UGF.EditorTools.Editor.Tests.Tools
{
    [EditorTool("TestToolComponentBoundsHandleCapsule", typeof(TestToolComponentBoundsHandleCapsuleComponent))]
    public class TestToolComponentBoundsHandleCapsule : ToolComponentBoundsHandleCapsule
    {
        public TestToolComponentBoundsHandleCapsule() : base("m_center", "m_height", "m_radius")
        {
        }
    }
}
