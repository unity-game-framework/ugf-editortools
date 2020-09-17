using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorObjectReferenceDrawer
    {
        public SerializedProperty SerializedProperty { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public EditorObjectReferenceDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
        }

        public void DrawGUILayout()
        {
            if (SerializedProperty.objectReferenceValue != null)
            {
                Drawer.Set(SerializedProperty.objectReferenceValue);
            }
            else
            {
                Drawer.Clear();
            }

            Drawer.DrawGUILayout();
        }
    }
}
