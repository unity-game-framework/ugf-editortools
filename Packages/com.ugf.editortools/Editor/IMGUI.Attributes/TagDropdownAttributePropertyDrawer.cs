using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(TagDropdownAttribute), true)]
    internal class TagDropdownAttributePropertyDrawer : PropertyDrawerTyped<TagDropdownAttribute>
    {
        public TagDropdownAttributePropertyDrawer() : base(SerializedPropertyType.String)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, serializedProperty);

            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            string value = EditorGUI.TagField(position, label, serializedProperty.stringValue);

            if (scope.Changed)
            {
                serializedProperty.stringValue = value;
            }

            EditorGUI.EndProperty();
        }
    }
}
