using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListSelectionDrawerByElement : ReorderableListSelectionDrawer
    {
        public ReorderableListSelectionDrawerByElement(ReorderableListDrawer listDrawer) : base(listDrawer)
        {
        }

        protected override SerializedProperty OnGetObjectReferenceProperty(SerializedProperty propertyElement)
        {
            return propertyElement;
        }
    }
}
