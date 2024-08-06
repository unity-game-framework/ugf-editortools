using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Search;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.IMGUI.Attributes
{
    [CustomPropertyDrawer(typeof(ObjectPickerAttribute), true)]
    internal class ObjectPickerAttributePropertyDrawer : PropertyDrawerTyped<ObjectPickerAttribute>
    {
        public ObjectPickerAttributePropertyDrawer() : base(SerializedPropertyType.ObjectReference)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            AttributeEditorGUIUtility.DrawObjectPickerField(position, label, serializedProperty, Attribute.TargetType, Attribute.Filter);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var element = new ObjectField
            {
                label = preferredLabel,
                bindingPath = property.propertyPath,
                objectType = Attribute.TargetType,
                searchViewFlags = SearchViewFlags.ObjectPicker | SearchViewFlags.CompactView | SearchViewFlags.IgnoreSavedSearches,
                searchContext = new SearchContext(SearchService.Providers, Attribute.Filter, SearchFlags.OpenPicker)
            };

            UIToolkitEditorUtility.AddFieldClasses(element);

            return element;
        }
    }
}
