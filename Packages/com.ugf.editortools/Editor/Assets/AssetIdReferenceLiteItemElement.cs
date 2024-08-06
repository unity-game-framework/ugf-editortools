using System;
using UGF.EditorTools.Editor.UIToolkit;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceLiteItemElement : VisualElement
    {
        public bool DisplayAsReplace
        {
            get { return m_displayAsReplace; }
            set
            {
                if (m_displayAsReplace != value)
                {
                    m_assetIdElement.EnableInClassList(UIToolkitEditorUssStyles.ElementDisplayNone, value);
                    m_replaceElement.EnableInClassList(UIToolkitEditorUssStyles.ElementDisplayNone, !value);
                    m_displayAsReplace = value;
                }
            }
        }

        private readonly AssetIdReferenceObjectFieldElement m_assetIdElement;
        private readonly KeyAndValueFieldElement m_replaceElement;
        private bool m_displayAsReplace;

        public AssetIdReferenceLiteItemElement()
        {
            m_assetIdElement = new AssetIdReferenceObjectFieldElement();
            m_replaceElement = new KeyAndValueFieldElement(new PropertyField { label = string.Empty }, new PropertyField { label = string.Empty });

            Add(m_assetIdElement);
            Add(m_replaceElement);

            m_replaceElement.AddToClassList(UIToolkitEditorUssStyles.ElementDisplayNone);

            UIToolkitEditorUtility.AddFieldClasses(m_assetIdElement);
            UIToolkitEditorUtility.AddStyleSheets(this);
        }

        public void Bind(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            m_assetIdElement.Bind(serializedProperty);

            SerializedProperty propertyKey = serializedProperty.FindPropertyRelative("m_guid");
            SerializedProperty propertyValue = serializedProperty.FindPropertyRelative("m_asset");

            var keyElement = (PropertyField)m_replaceElement.KeyElement;
            var valueElement = (PropertyField)m_replaceElement.ValueElement;

            keyElement.bindingPath = propertyKey.propertyPath;
            valueElement.bindingPath = propertyValue.propertyPath;

            keyElement.BindProperty(propertyKey);
            valueElement.BindProperty(propertyValue);
        }

        public void Unbind(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            m_assetIdElement.Unbind(serializedProperty);

            var keyElement = (PropertyField)m_replaceElement.KeyElement;
            var valueElement = (PropertyField)m_replaceElement.ValueElement;

            keyElement.Unbind();
            valueElement.Unbind();

            keyElement.bindingPath = string.Empty;
            valueElement.bindingPath = string.Empty;
        }
    }
}
