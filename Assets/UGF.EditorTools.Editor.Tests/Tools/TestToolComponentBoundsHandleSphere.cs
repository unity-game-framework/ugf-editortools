using UGF.EditorTools.Editor.Tools;
using UGF.EditorTools.Runtime.Tests.Tools;
using UnityEditor.EditorTools;

namespace UGF.EditorTools.Editor.Tests.Tools
{
    [EditorTool("TestToolComponentBoundsHandleSphere", typeof(TestToolComponentBoundsHandleSphereComponent))]
    public class TestToolComponentBoundsHandleSphere : ToolComponentBoundsHandleSphere
    {
        public TestToolComponentBoundsHandleSphere() : base("m_center", "m_radius")
        {
        }
    }
}
