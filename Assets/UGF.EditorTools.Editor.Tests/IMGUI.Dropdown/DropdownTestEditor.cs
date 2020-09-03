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
                if (DropdownEditorGUIUtility.Dropdown(Name, m_item?.Name ?? "None", Items, out DropdownItem item))
                {
                    m_item = item;
                }
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
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownEditorGUIUtility.DropdownButton("Dropdown With Position", "None", out Rect dropdownPosition))
            {
                m_dropdown.Show(dropdownPosition);
            }

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownEditorGUIUtility.DropdownButton("None", out Rect dropdownPosition2))
            {
                m_dropdown.Show(dropdownPosition2);
            }

            EditorGUILayout.LayerField("Test", 0);

            EditorGUILayout.LabelField("Test", EditorStyles.boldLabel);

            for (int i = 0; i < m_drawers.Count; i++)
            {
                m_drawers[i].Draw();
            }
        }
    }
}
