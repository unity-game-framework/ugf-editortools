using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class IconButtonElement : Button
    {
        public static string UssClassName { get; } = "ugf-icon-button";

        public IconButtonElement()
        {
            AddToClassList(UssClassName);

            UIToolkitEditorUtility.AddStyleSheets(this);
        }
    }
}
