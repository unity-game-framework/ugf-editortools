using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI;
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
        private static Styles m_styles;

        private class Styles
        {
            public GUIContent FieldIconReferenceContent { get; } = EditorGUIUtility.IconContent("UnityEditor.FindDependencies");
        }

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
            m_styles ??= new Styles();

            var element = new ObjectField(preferredLabel)
            {
                objectType = Attribute.AssetType
            };

            VisualElement label = element.Query<VisualElement>(className: "unity-object-field-display__label").First();

            label.Add(new Button(Background.FromTexture2D((Texture2D)m_styles.FieldIconReferenceContent.image))
            {
                tooltip = "Tooltip",
                style =
                {
                    width = EditorGUIUtility.singleLineHeight,
                    height = EditorGUIUtility.singleLineHeight
                }
            });

            UIToolkitEditorUtility.AddFieldClasses(element);

            UIToolkitPropertyBindingField<Object>.Bind(
                element,
                property,
                (_, serializedProperty) =>
                {
                    string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                    if (!string.IsNullOrEmpty(path) && asset == null)
                    {
                        asset = EditorIMGUIUtility.MissingObject;
                    }

                    return asset;
                },
                (_, serializedProperty, value) =>
                {
                    if (!EditorIMGUIUtility.IsMissingObject(value))
                    {
                        string path = AssetDatabase.GetAssetPath(value);
                        string guid = AssetDatabase.AssetPathToGUID(path);

                        GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
                    }

                    return value;
                }
            );

            return element;
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
