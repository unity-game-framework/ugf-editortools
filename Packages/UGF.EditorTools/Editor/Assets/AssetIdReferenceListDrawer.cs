using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceListDrawer : ReorderableListDrawer
    {
        public AssetIdReferenceListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            AssetIdReferenceEditorGUIUtility.AssetIdReferenceField(position, GUIContent.none, serializedProperty);
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
