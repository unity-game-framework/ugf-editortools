using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PropertyDrawers
{
    public abstract class PropertyDrawerBase : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, serializedProperty);

            OnGUIProperty(position, serializedProperty, label);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(serializedProperty, label, true);
        }

        protected virtual void OnGUIProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            OnDrawProperty(position, serializedProperty, label);
        }

        protected abstract void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label);

        protected virtual void OnDrawPropertyDefault(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorGUI.PropertyField(position, serializedProperty, label, true);
        }
    }
}
