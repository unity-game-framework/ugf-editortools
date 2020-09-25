using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.EnabledProperty
{
    public class EnabledPropertyListDrawer : ReorderableListDrawer
    {
        public EnabledPropertyListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            if (propertyValue.propertyType == SerializedPropertyType.ObjectReference)
            {
                EnabledPropertyGUIUtility.EnabledProperty(position, GUIContent.none, serializedProperty);
            }
            else
            {
                base.OnDrawElementContent(position, serializedProperty, index, isActive, isFocused);
            }
        }
    }
}
