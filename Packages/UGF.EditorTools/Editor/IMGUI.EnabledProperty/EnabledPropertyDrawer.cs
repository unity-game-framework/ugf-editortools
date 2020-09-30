using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.EnabledProperty
{
    [CustomPropertyDrawer(typeof(EnabledProperty<>))]
    internal class EnabledPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EnabledPropertyGUIUtility.EnabledProperty(position, label, serializedProperty);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_value");

            return base.GetPropertyHeight(propertyValue, label);
        }
    }
}
