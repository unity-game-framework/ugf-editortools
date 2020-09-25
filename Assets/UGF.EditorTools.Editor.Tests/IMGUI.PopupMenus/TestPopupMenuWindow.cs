using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PopupMenus;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.PopupMenus
{
    public class TestPopupMenuWindow : EditorWindow
    {
        private PopupMenuContent m_content;

        [MenuItem("Tests/TestPopupMenuWindow")]
        private static void Menu()
        {
            GetWindow<TestPopupMenuWindow>();
        }

        private void OnEnable()
        {
            m_content = new PopupMenuContent();

            for (int i = 0; i < 10; i++)
            {
                m_content.Items.Add(new PopupMenuItem(new GUIContent($"Item {i}")));
            }
        }

        private void OnGUI()
        {
            if (DropdownEditorGUIUtility.DropdownButton(new GUIContent("Menu"), new GUIContent("Content"), out Rect dropdownPosition))
            {
                PopupWindow.Show(dropdownPosition, m_content);
            }
        }
    }
}
