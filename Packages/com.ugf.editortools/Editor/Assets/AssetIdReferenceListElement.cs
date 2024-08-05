using UGF.EditorTools.Editor.UIToolkit.Elements;
using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdReferenceListElement : ListElement
    {
        public bool DisplayAsReplace { get; set; }
        public bool DisplayReplaceButton { get; set; } = true;

        public AssetIdReferenceListElement(SerializedProperty serializedProperty, bool field) : base(serializedProperty, field)
        {
            makeItem = OnCreateItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;
        }

        public AssetIdReferenceListElement()
        {
            makeItem = OnCreateItem;
            bindItem = OnBindItem;
            unbindItem = OnUnbindItem;
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
