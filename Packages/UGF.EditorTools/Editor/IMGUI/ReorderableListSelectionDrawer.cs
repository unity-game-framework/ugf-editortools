using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public abstract class ReorderableListSelectionDrawer : DrawerBase
    {
        public ReorderableListDrawer ListDrawer { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        protected ReorderableListSelectionDrawer(ReorderableListDrawer listDrawer)
        {
            ListDrawer = listDrawer ?? throw new ArgumentNullException(nameof(listDrawer));
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            ListDrawer.SelectionUpdated += OnSelectionUpdated;
            Drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ListDrawer.SelectionUpdated -= OnSelectionUpdated;
            Drawer.Disable();
        }

        protected abstract SerializedProperty OnGetObjectReferenceProperty(SerializedProperty propertyElement);

        public void DrawGUILayout()
        {
            Drawer.DrawGUILayout();
        }

        private void OnSelectionUpdated()
        {
            if (ListDrawer.List.index >= 0 && ListDrawer.List.index < ListDrawer.List.count)
            {
                SerializedProperty propertyElement = ListDrawer.SerializedProperty.GetArrayElementAtIndex(ListDrawer.List.index);
                SerializedProperty serializedProperty = OnGetObjectReferenceProperty(propertyElement);

                if (serializedProperty.objectReferenceValue != null)
                {
                    Drawer.Set(serializedProperty.objectReferenceValue);
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
    }
}
