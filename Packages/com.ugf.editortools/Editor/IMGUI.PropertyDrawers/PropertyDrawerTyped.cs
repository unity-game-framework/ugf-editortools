using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.PropertyDrawers
{
    public abstract class PropertyDrawerTyped<TAttribute> : PropertyDrawer<TAttribute> where TAttribute : PropertyAttribute
    {
        public SerializedPropertyType PropertyType { get; }

        protected PropertyDrawerTyped(SerializedPropertyType propertyType)
        {
            PropertyType = propertyType;
        }

        protected override void OnGUIProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            if (serializedProperty.propertyType == PropertyType)
            {
                base.OnGUIProperty(position, serializedProperty, label);
            }
            else
            {
                OnDrawPropertyDefault(position, serializedProperty, label);

                Debug.LogWarning($"Invalid type of specified serialized property: '{serializedProperty.propertyPath} ({serializedProperty.propertyType})', must be '{PropertyType}' in order to use '{typeof(TAttribute)}'.");
            }
        }
    }
}
