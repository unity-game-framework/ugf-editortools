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

        protected override void OnGUIProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == PropertyType)
            {
                base.OnGUIProperty(position, property, label);
            }
            else
            {
                OnDrawPropertyDefault(position, property, label);

                Debug.LogWarning($"Invalid type of specified serialized property: '{property.propertyPath} ({property.propertyType})', must be '{PropertyType}' in order to use '{typeof(TAttribute)}'.");
            }
        }
    }
}
