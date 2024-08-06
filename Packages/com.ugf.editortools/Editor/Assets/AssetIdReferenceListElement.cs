using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceListElement : ListElement
    {
        public bool DisplayAsReplace { get; set; }
        public bool DisplayReplaceButton { get; set; } = true;

        private IconButtonElement m_replaceButton;

        public AssetIdReferenceListElement(SerializedProperty serializedProperty, bool field) : base(serializedProperty, field)
        {
            m_replaceButton = new IconButtonElement
            {
                iconImage = Background.FromTexture2D(EditorGUIUtility.FindTexture("preAudioLoopOff"))
            };

            makeItem = OnCreateItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;

            VisualElement footer = this.Query(footerUssClassName).First();

            footer.Insert(0, m_replaceButton);
        }

        public AssetIdReferenceListElement()
        {
            m_replaceButton = new IconButtonElement();

            makeItem = OnCreateItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;

            VisualElement footer = this.Query(footerUssClassName).First();

            footer.Insert(0, m_replaceButton);
        }

        private VisualElement OnCreateItem()
        {
            return new AssetIdReferenceLiteItemElement();
        }

        private void OnBindItem(VisualElement element, int index)
        {
            if (HasSerializedProperty)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

                var itemElement = (AssetIdReferenceLiteItemElement)element;

                itemElement.Bind(propertyElement);
            }
        }

        private void OnUnbindItem(VisualElement element, int index)
        {
            if (HasSerializedProperty)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(index);

                var itemElement = (AssetIdReferenceLiteItemElement)element;

                itemElement.Unbind(propertyElement);
            }
        }
    }
}
