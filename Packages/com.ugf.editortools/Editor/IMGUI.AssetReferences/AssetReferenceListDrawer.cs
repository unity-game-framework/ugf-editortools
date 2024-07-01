using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.AssetReferences
{
    public class AssetReferenceListDrawer : ReorderableListDrawer
    {
        public AssetReferenceListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            AssetReferenceEditorGUIUtility.AssetReference(position, GUIContent.none, serializedProperty);
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
