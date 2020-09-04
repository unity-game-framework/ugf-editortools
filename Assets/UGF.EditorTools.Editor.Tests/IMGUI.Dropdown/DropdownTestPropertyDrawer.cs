using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Dropdown
{
    public class DropdownTestAttribute : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(DropdownTestAttribute), true)]
    public class DropdownTestPropertyDrawer : PropertyDrawer
    {
        private static readonly DropdownSelection<DropdownItem<string>> m_selection = new DropdownSelection<DropdownItem<string>>();

        private static readonly List<DropdownItem<string>> m_items = new List<DropdownItem<string>>
        {
            new DropdownItem<string>("Item 0", "Value 0"),
            new DropdownItem<string>("Item 1", "Value 1"),
            new DropdownItem<string>("Item 2", "Value 2")
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DropdownItem<string> item = m_items.Find(x => x.Value == property.stringValue) ?? new DropdownItem<string>("None", "");

            DropdownItem<string> selected = DropdownEditorGUIUtility.Dropdown(position, label, new GUIContent(item.Name), m_selection, m_items, item);

            if (item.Value != selected.Value)
            {
                property.stringValue = selected.Value;
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
