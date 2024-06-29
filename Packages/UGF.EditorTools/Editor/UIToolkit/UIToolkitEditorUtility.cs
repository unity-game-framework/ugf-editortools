using System;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public static class UIToolkitEditorUtility
    {
        public static void AddFieldClasses<T>(BaseField<T> field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            field.AddToClassList(BaseField<T>.alignedFieldUssClassName);
        }
    }
}
