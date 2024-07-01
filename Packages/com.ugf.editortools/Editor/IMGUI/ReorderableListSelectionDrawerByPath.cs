using System;
using UnityEditor;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListSelectionDrawerByPath : ReorderableListSelectionDrawer
    {
        public string PropertyPath { get; }

        public ReorderableListSelectionDrawerByPath(ReorderableListDrawer listDrawer, string propertyPath) : base(listDrawer)
        {
            if (string.IsNullOrEmpty(propertyPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(propertyPath));

            PropertyPath = propertyPath;
        }

        protected override SerializedProperty OnGetObjectReferenceProperty(SerializedProperty propertyElement)
        {
            return propertyElement.FindPropertyRelative(PropertyPath);
        }
    }
}
