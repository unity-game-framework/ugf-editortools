using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.IMGUI
{
    public class ReorderableListSelectionDrawerByElementGlobalId : ReorderableListSelectionDrawerByElement
    {
        public ReorderableListSelectionDrawerByElementGlobalId(ReorderableListDrawer listDrawer) : base(listDrawer)
        {
        }

        protected override bool OnTryGetTarget(SerializedProperty serializedProperty, out Object target)
        {
            string guid = serializedProperty.propertyType == SerializedPropertyType.Hash128
                ? GlobalId.FromHash128(serializedProperty.hash128Value).ToString()
                : GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            target = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));

            return target != null;
        }
    }
}
