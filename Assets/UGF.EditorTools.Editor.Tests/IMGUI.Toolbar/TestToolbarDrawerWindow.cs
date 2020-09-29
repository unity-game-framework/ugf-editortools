using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Toolbar
{
    public class TestToolbarDrawerWindow : EditorWindow
    {
        private readonly ToolbarDrawer m_drawer1 = new ToolbarDrawer { Count = 5 };
        private readonly ToolbarDrawer m_drawer2 = new ToolbarDrawer { Count = 5, Styles = new ToolbarStyles("Tab onlyOne", "Tab first", "Tab middle", "Tab last") };
        private readonly ToolbarDrawer m_drawer3 = new ToolbarDrawer { Count = 5, Direction = ToolbarDirection.Vertical };

        [MenuItem("Tests/TestToolbarDrawerWindow")]
        private static void Menu()
        {
            GetWindow<TestToolbarDrawerWindow>();
        }

        private void OnGUI()
        {
            m_drawer1.DrawGUILayout();
            m_drawer2.DrawGUILayout();
            m_drawer3.DrawGUILayout();
        }
    }
}
