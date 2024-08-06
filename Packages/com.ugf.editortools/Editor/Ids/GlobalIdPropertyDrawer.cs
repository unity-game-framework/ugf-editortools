using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Ids
{
    [CustomPropertyDrawer(typeof(GlobalId), true)]
    internal class GlobalIdPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            guid = EditorGUI.TextField(position, label, guid);

            if (scope.Changed)
            {
                GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new GlobalIdFieldElement(property, true)
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
