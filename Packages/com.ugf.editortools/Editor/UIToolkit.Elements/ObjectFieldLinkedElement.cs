using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ObjectFieldLinkedElement : ObjectField
    {
        public IconButtonElement LinkButtonElement { get; }
        public static string LinkButtonUssClassName { get; } = "ugf-object-field-linked__button";

        public ObjectFieldLinkedElement()
        {
            LinkButtonElement = new IconButtonElement();
            LinkButtonElement.AddToClassList(LinkButtonUssClassName);

            VisualElement inputElement = this.Query<VisualElement>(className: inputUssClassName).First();
            VisualElement selectorElement = inputElement.Query<VisualElement>(className: selectorUssClassName).First();

            inputElement.Insert(inputElement.IndexOf(selectorElement), LinkButtonElement);

            UIToolkitEditorUtility.AddStyleSheets(this);
        }
    }
}
