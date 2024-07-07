﻿using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    [CustomPropertyDrawer(typeof(AssetIdAttribute))]
    internal class AssetIdAttributePropertyDrawer : PropertyDrawerTyped<AssetIdAttribute>
    {
        public AssetIdAttributePropertyDrawer() : base(SerializedPropertyType.Generic)
        {
        }

        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            using var scope = new MixedValueChangedScope(serializedProperty.hasMultipleDifferentValues);

            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            guid = AttributeEditorGUIUtility.DrawAssetGuidField(position, guid, label, Attribute.AssetType);

            if (scope.Changed)
            {
                GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var element = new AssetIdObjectFieldElement()
            {
                label = preferredLabel,
                objectType = Attribute.AssetType
            };

            UIToolkitEditorUtility.AddFieldClasses(element);

            element.PropertyBinding.Bind(property);
            element.TrackPropertyValue(property);

            return element;
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
