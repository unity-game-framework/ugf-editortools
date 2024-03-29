﻿using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorListDrawer : ReorderableListDrawer
    {
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public EditorListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
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

        protected override void OnSelectionUpdate()
        {
            base.OnSelectionUpdate();

            if (List.index >= 0 && List.index < List.count)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(List.index);

                if (propertyElement.objectReferenceValue != null)
                {
                    Drawer.Set(propertyElement.objectReferenceValue);
                }
                else
                {
                    Drawer.Clear();
                }
            }
            else
            {
                Drawer.Clear();
            }
        }

        public void DrawSelectedLayout()
        {
            Drawer.DrawGUILayout();
        }
    }
}
