using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(SelectDirectoryAttribute), true)]
    internal class SelectDirectoryAttributePropertyDrawer : PropertyDrawerTyped<SelectDirectoryAttribute>
    {
        public SelectDirectoryAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawSelectDirectoryField(position, serializedProperty, label, Attribute.Title, Attribute.Directory, Attribute.InAssets);
        }
    }
}
