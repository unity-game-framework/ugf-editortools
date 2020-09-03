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

        private void OnEnable()
        {
            for (int i = 0; i < 10; i++)
            {
                m_dropdown.Items.Add(new DropdownItem($"Item {i}"));
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownGUIUtility.DropdownButton("Dropdown With Position", "None", out Rect dropdownPosition))
            {
                m_dropdown.Show(dropdownPosition);
            }

            EditorGUILayout.LayerField("Test", 0);

            if (DropdownGUIUtility.DropdownButton("None", out Rect dropdownPosition2))
            {
                m_dropdown.Show(dropdownPosition2);
            }

            EditorGUILayout.LayerField("Test", 0);
        }
    }
}
