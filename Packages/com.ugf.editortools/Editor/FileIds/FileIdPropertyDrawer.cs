using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.FileIds
{
    [CustomPropertyDrawer(typeof(Runtime.FileIds.FileId), true)]
    internal class FileIdPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            FileIdEditorGUIUtility.DrawFileIdField(position, label, serializedProperty, typeof(Object));
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
