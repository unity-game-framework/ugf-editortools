using UGF.EditorTools.Editor.Ids;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListSelectionDrawerByPathGlobalId : ReorderableListSelectionDrawerByPath
    {
        public ReorderableListSelectionDrawerByPathGlobalId(ReorderableListDrawer listDrawer, string propertyPath) : base(listDrawer, propertyPath)
        {
        }

        protected override bool OnTryGetTarget(SerializedProperty serializedProperty, out Object target)
        {
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            target = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));

            return target != null;
        }
    }
}
