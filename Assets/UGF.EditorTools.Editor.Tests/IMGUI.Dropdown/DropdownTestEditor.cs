using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Dropdown
{
    [CustomEditor(typeof(DropdownTestAsset), true)]
    public class DropdownTestEditor : UnityEditor.Editor
    {
        private readonly Dropdown<DropdownItem> m_dropdown = new Dropdown<DropdownItem>
        {
            MinimumHeight = 250F
        };

        private readonly List<DropdownDrawerTest> m_drawers = new List<DropdownDrawerTest>();
        private readonly List<string> m_values = new List<string>();
        private readonly List<DropdownItem<string>> m_valueItems = new List<DropdownItem<string>>();

        private class DropdownDrawerTest
        {
            public string Name { get; }
            public List<DropdownItem> Items { get; } = new List<DropdownItem>();

            private DropdownItem m_item;

            public DropdownDrawerTest(string name)
            {
                Name = name;

                for (int i = 0; i < 10; i++)
                {
                    Items.Add(new DropdownItem($"{Name} Item {i}"));
                }
            }

            public void Draw()
            {
                m_item = DropdownEditorGUIUtility.Dropdown(new GUIContent(Name), new GUIContent(m_item?.Name ?? "None"), Items, m_item);
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < 10; i++)
            {
                m_dropdown.Items.Add(new DropdownItem($"Item {i}"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_drawers.Add(new DropdownDrawerTest($"Drawer {i}"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_values.Add("None");
            }

            for (int i = 0; i < 10; i++)
            {
                m_valueItems.Add(new DropdownItem<string>($"Item {i}", $"Value {i}"));
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownEditorGUIUtility.DropdownButton(new GUIContent("Dropdown With Position"), new GUIContent("None"), out Rect dropdownPosition))
            {
                m_dropdown.Show(dropdownPosition);
            }

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownEditorGUIUtility.DropdownButton(GUIContent.none, new GUIContent("None"), out Rect dropdownPosition2))
            {
                m_dropdown.Show(dropdownPosition2);
            }

            EditorGUILayout.LayerField("Test", 0);

            EditorGUILayout.LabelField("Test", EditorStyles.boldLabel);

            for (int i = 0; i < m_drawers.Count; i++)
            {
                m_drawers[i].Draw();
            }

            for (int i = 0; i < m_values.Count; i++)
            {
                m_values[i] = DropdownEditorGUIUtility.Dropdown(new GUIContent($"Test {i}"), new GUIContent($"{m_values[i]}"), m_valueItems, m_valueItems[i]).Value;
            }
        }
    }
}
