using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(DisableAttribute), true)]
    public class DisableAttributeDrawer : PropertyDrawer<DisableAttribute>
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.DisabledScope(true))
            {
                OnDrawPropertyDefault(position, property, label);
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property, preferredLabel)
            {
                enabledSelf = false
            };
        }
    }
}
