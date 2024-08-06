using UGF.EditorTools.Editor.FileIds;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(FileIdAttribute), true)]
    internal class FileIdAttributePropertyDrawer : PropertyDrawer<FileIdAttribute>
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            if (serializedProperty.propertyType == SerializedPropertyType.String)
            {
                AttributeEditorGUIUtility.DrawFileIdField(position, label, serializedProperty, Attribute.AssetType);
            }
            else
            {
                FileIdEditorGUIUtility.DrawFileIdField(position, label, serializedProperty, Attribute.AssetType);
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
