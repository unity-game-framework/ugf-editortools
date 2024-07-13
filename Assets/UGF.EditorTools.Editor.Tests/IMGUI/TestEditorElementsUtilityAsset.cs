using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestEditorElementsUtilityAsset")]
    public class TestEditorElementsUtilityAsset : ScriptableObject
    {
        [SerializeField] private string m_value;
        [SerializeField] private string m_value2;
        [SerializeField] private string m_value3;
        [TimeTicks]
        [SerializeField] private long m_time;
        [TimeSpanTicks]
        [SerializeField] private long m_time2;

        public string Value { get { return m_value; } set { m_value = value; } }
        public string Value2 { get { return m_value2; } set { m_value2 = value; } }
        public string Value3 { get { return m_value3; } set { m_value3 = value; } }
        public long Time { get { return m_time; } set { m_time = value; } }
        public long Time2 { get { return m_time2; } set { m_time2 = value; } }
    }

    [CanEditMultipleObjects]
    // [CustomEditor(typeof(TestEditorElementsUtilityAsset), true)]
    public class TestEditorElementsUtilityAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyValue;
        private SerializedProperty m_propertyValue2;
        private SerializedProperty m_propertyValue3;
        private SerializedProperty m_propertyTime;
        private SerializedProperty m_propertyTime2;

        private void OnEnable()
        {
            m_propertyValue = serializedObject.FindProperty("m_value");
            m_propertyValue2 = serializedObject.FindProperty("m_value2");
            m_propertyValue3 = serializedObject.FindProperty("m_value3");
            m_propertyTime = serializedObject.FindProperty("m_time");
            m_propertyTime2 = serializedObject.FindProperty("m_time2");
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorElementsUtility.TextFieldWithDropdown(m_propertyValue, OnGetItems);
                EditorElementsUtility.TextFieldWithDropdown(m_propertyValue2, OnGetItems2);

                using (new IndentLevelScope(2))
                {
                    EditorElementsUtility.TextFieldWithDropdown(m_propertyValue3, OnGetItems2);
                }

                EditorGUILayout.PropertyField(m_propertyTime);
                EditorGUILayout.PropertyField(m_propertyTime2);
            }
        }

        private IEnumerable<DropdownItem<string>> OnGetItems()
        {
            var items = new List<DropdownItem<string>>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(new DropdownItem<string>($"Value {i}", $"Value {i}"));
            }

            return items;
        }

        private IEnumerable<DropdownItem<string>> OnGetItems2()
        {
            var items = new List<DropdownItem<string>>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(new DropdownItem<string>($"Item {i}", $"Item {i}"));
            }

            return items;
        }
    }
}
