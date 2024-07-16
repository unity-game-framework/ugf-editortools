using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit.Dropdown
{
    public class DropdownButtonElement : Button
    {
        public DropdownButtonElement()
        {
            iconImage = Background.FromTexture2D(EditorGUIUtility.FindTexture("icon dropdown"));

            VisualElement imageElement = this.Query(className: imageUSSClassName).First();
            TextElement textElement = this.Query(className: ussClassName).Children<TextElement>().First();

            Remove(imageElement);
            Add(imageElement);

            imageElement.AddToClassList(BasePopupField<bool, bool>.arrowUssClassName);
            textElement.AddToClassList(BasePopupField<bool, bool>.textUssClassName);
        }
    }
}
