using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ObjectFieldLinkedElement : ObjectField
    {
        public Button LinkButtonElement { get; }
        public static string LinkButtonUssClassName { get; } = "ugf-object-field-linked__button";

        public ObjectFieldLinkedElement()
        {
            LinkButtonElement = new Button();
            LinkButtonElement.AddToClassList(LinkButtonUssClassName);

            VisualElement inputElement = this.Query<VisualElement>(className: inputUssClassName).First();
            VisualElement selectorElement = inputElement.Query<VisualElement>(className: selectorUssClassName).First();

            inputElement.Insert(inputElement.IndexOf(selectorElement), LinkButtonElement);
            styleSheets.Add(UIToolkitEditorUtility.StyleSheet);
        }
    }
}
