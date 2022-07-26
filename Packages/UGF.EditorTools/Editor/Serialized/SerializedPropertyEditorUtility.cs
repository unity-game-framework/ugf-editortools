using System;
using System.Collections.Generic;
using UnityEditor;

namespace UGF.EditorTools.Editor.Serialized
{
    public static class SerializedPropertyEditorUtility
    {
        public static IEnumerable<SerializedProperty> GetChildren(SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            SerializedProperty propertyEnd = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            if (!SerializedProperty.EqualContents(serializedProperty, propertyEnd))
            {
                yield return serializedProperty;

                while (serializedProperty.NextVisible(false) && !SerializedProperty.EqualContents(serializedProperty, propertyEnd))
                {
                    yield return serializedProperty;
                }
            }
        }
    }
}
