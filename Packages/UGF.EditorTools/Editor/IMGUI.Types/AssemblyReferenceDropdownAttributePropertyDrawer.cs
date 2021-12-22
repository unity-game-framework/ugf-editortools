using System.Collections.Generic;
using System.Reflection;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Types
{
    [CustomPropertyDrawer(typeof(AssemblyReferenceDropdownAttribute), true)]
    internal class AssemblyReferenceDropdownAttributePropertyDrawer : PropertyDrawerTyped<AssemblyReferenceDropdownAttribute>
    {
        protected DropdownDrawer<DropdownItem<Assembly>> Drawer { get { return m_drawer ??= OnCreateDrawer(); } }

        protected DropdownItem<Assembly> NoneItem { get; } = new DropdownItem<Assembly>("None")
        {
            Priority = int.MaxValue
        };

        private DropdownDrawer<DropdownItem<Assembly>> m_drawer;

        public AssemblyReferenceDropdownAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            if (propertyValue != null)
            {
                Drawer.DrawGUI(position, label, propertyValue);
            }
            else
            {
                OnDrawPropertyDefault(position, serializedProperty, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private DropdownDrawer<DropdownItem<Assembly>> OnCreateDrawer()
        {
            return new AssemblyDropdownDrawer(OnGetItems);
        }

        private IEnumerable<DropdownItem<Assembly>> OnGetItems()
        {
            var items = new List<DropdownItem<Assembly>>
            {
                NoneItem
            };

            AssemblyDropdownEditorUtility.GetAssemblyItems(items, Attribute.DisplayFullPath);

            return items;
        }
    }
}
