using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public delegate TValue UIToolkitPropertyBindingFieldUpdateHandler<TValue>(BaseField<TValue> field, SerializedProperty serializedProperty);
}
