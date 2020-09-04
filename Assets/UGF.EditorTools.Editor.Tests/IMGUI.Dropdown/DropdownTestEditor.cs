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

        private readonly List<DropdownItem<string>> m_values = new List<DropdownItem<string>>();
        private readonly List<DropdownItem<string>> m_valueItems = new List<DropdownItem<string>>();
        private readonly DropdownSelection<DropdownItem<string>> m_valuesSelection = new DropdownSelection<DropdownItem<string>>();
        private readonly List<DropdownItem<object>> m_values2 = new List<DropdownItem<object>>();
        private readonly List<DropdownItem<object>> m_valueItems2 = new List<DropdownItem<object>>();

        private void OnEnable()
        {
            for (int i = 0; i < 10; i++)
            {
                m_dropdown.Items.Add(new DropdownItem($"Item {i}"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_values.Add(new DropdownItem<string>("None"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_valueItems.Add(new DropdownItem<string>($"Item {i}", $"Value {i}"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_values2.Add(new DropdownItem<object>("None"));
            }

            for (int i = 0; i < 10; i++)
            {
                m_valueItems2.Add(new DropdownItem<object>($"Item {i}", $"Value {i}")
                {
                    Path = new []
                    {
                        "Path 0",
                        "Path 1",
                        "Path 2"
                    }
                });
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

            for (int i = 0; i < m_values.Count; i++)
            {
                m_values[i] = DropdownEditorGUIUtility.Dropdown(new GUIContent($"Test {i}"), new GUIContent($"{m_values[i].Value}"), m_valuesSelection, m_valueItems, m_values[i]);
            }

            for (int i = 0; i < m_values2.Count; i++)
            {
                m_values2[i] = DropdownEditorGUIUtility.Dropdown(new GUIContent($"Test2 {i}"), new GUIContent($"{m_values2[i].Value}"), m_valueItems2, m_values2[i]);
            }
        }
    }
}
