using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.AssetReferences
{
    [CustomPropertyDrawer(typeof(AssetReference<>), true)]
    internal class AssetReferencePropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AssetReferenceEditorGUIUtility.AssetReference(position, label, serializedProperty);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
