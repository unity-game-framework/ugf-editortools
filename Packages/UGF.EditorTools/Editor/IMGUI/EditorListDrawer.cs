using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class EditorListDrawer : ReorderableListDrawer
    {
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public EditorListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnRemove()
        {
            base.OnRemove();

            UpdateSelection();
        }

        protected override void OnSelect()
        {
            base.OnSelect();

            UpdateSelection();
        }

        public void DrawSelectedLayout()
        {
            Drawer.DrawGUILayout();
        }

        private void UpdateSelection()
        {
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
    }
}
