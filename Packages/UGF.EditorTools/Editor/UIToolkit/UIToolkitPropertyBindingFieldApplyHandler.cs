using UnityEditor;
using UnityEngine.UIElements;

namespace UGF.EditorTools.Editor.UIToolkit
{
    public delegate TValue UIToolkitPropertyBindingFieldApplyHandler<TValue>(BaseField<TValue> field, SerializedProperty serializedProperty, TValue value);
}
