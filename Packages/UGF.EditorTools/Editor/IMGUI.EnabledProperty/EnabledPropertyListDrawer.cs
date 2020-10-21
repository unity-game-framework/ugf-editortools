using UGF.EditorTools.Editor.IMGUI.Scopes;
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
                float width = EditorIMGUIUtility.GetIndentWithLevel(1);

                using (new LabelWidthChangeScope(width))
                {
                    base.OnDrawElementContent(position, serializedProperty, index, isActive, isFocused);
                }
            }
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
