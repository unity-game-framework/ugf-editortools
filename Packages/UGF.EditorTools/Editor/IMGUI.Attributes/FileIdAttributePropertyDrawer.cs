using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(FileIdAttribute), true)]
    internal class FileIdAttributePropertyDrawer : PropertyDrawerTyped<FileIdAttribute>
    {
        public FileIdAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawFileIdField(position, label, serializedProperty, Attribute.AssetType);
        }
    }
}
