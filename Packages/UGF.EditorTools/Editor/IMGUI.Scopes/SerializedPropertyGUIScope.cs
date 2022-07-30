using System;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI.Scopes
{
    public readonly struct SerializedPropertyGUIScope : IDisposable
    {
        public SerializedPropertyGUIScope(Rect position, GUIContent label, SerializedProperty serializedProperty)
        {
            EditorGUI.BeginProperty(position, label, serializedProperty);
        }

        public void Dispose()
        {
            EditorGUI.EndProperty();
        }
    }
}
