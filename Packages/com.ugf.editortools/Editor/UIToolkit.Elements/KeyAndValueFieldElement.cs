using System;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class KeyAndValueFieldElement : VisualElement
    {
        public VisualElement KeyElement { get; }
        public VisualElement ValueElement { get; }

        public static string UssClassName { get; } = "ugf-key-and-value-field";
        public static string UssClassNameKey { get; } = "ugf-key-and-value-field__key";
        public static string UssClassNameValue { get; } = "ugf-key-and-value-field__value";

        public KeyAndValueFieldElement(VisualElement keyElement, VisualElement valueElement)
        {
            KeyElement = keyElement ?? throw new ArgumentNullException(nameof(keyElement));
            ValueElement = valueElement ?? throw new ArgumentNullException(nameof(valueElement));

            Add(KeyElement);
            Add(ValueElement);

            AddToClassList(UssClassName);

            KeyElement.AddToClassList(UssClassNameKey);
            ValueElement.AddToClassList(UssClassNameValue);

            UIToolkitEditorUtility.AddStyleSheets(this);

            KeyElement.AddManipulator(new UIToolkitFieldLabelAlignManipulator());
        }
    }
}
