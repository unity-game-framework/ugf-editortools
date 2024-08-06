using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    [CustomPropertyDrawer(typeof(AssetIdReference<>), true)]
    internal class AssetIdReferencePropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AssetIdReferenceEditorGUIUtility.AssetIdReferenceField(position, label, serializedProperty);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new AssetIdReferenceObjectFieldElement(property, true)
            {
                label = preferredLabel
            };
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
