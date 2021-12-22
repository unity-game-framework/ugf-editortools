using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(SelectFileAttribute), true)]
    internal class SelectFileAttributePropertyDrawer : PropertyDrawerTyped<SelectFileAttribute>
    {
        public SelectFileAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            serializedProperty.stringValue = AttributeEditorGUIUtility.DrawSelectFileField(position, label, serializedProperty.stringValue, Attribute.Title, Attribute.Directory, Attribute.Extension, Attribute.InAssets);
        }
    }
}
