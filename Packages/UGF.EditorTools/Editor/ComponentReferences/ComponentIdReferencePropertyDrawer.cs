using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.ComponentReferences;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.ComponentReferences
{
    [CustomPropertyDrawer(typeof(ComponentIdReference<>), true)]
    internal class ComponentIdReferencePropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            ComponentReferenceEditorGUIUtility.DrawIdReferenceField(position, label, serializedProperty);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
