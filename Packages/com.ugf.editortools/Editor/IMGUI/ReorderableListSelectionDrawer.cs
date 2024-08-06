using System;
using UnityEditor;
using Object = UnityEngine.Object;

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

        public void DrawGUILayout()
        {
            Drawer.DrawGUILayout();
        }

        protected abstract SerializedProperty OnGetObjectReferenceProperty(SerializedProperty propertyElement);

        protected virtual bool OnTryGetTarget(SerializedProperty serializedProperty, out Object target)
        {
            target = serializedProperty.objectReferenceValue;
            return target != null;
        }

        private void OnSelectionUpdated()
        {
            if (ListDrawer.List.index >= 0 && ListDrawer.List.index < ListDrawer.List.count)
            {
                SerializedProperty propertyElement = ListDrawer.SerializedProperty.GetArrayElementAtIndex(ListDrawer.List.index);
                SerializedProperty serializedProperty = OnGetObjectReferenceProperty(propertyElement);

                if (OnTryGetTarget(serializedProperty, out Object target))
                {
                    Drawer.Set(target);
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
