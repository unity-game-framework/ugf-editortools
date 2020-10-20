using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorObjectReferenceDrawer : DrawerBase
    {
        public SerializedProperty SerializedProperty { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public EditorObjectReferenceDrawer(SerializedProperty serializedProperty)
        {
            SerializedProperty = serializedProperty ?? throw new ArgumentNullException(nameof(serializedProperty));
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Drawer.Disable();
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
