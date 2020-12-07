using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Ids
{
    [CustomPropertyDrawer(typeof(GlobalId), true)]
    internal class GlobalIdPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            string guid = GlobalIdEditorUtility.GetGuidFromProperty(serializedProperty);

            guid = EditorGUI.TextField(position, label, guid);

            GlobalIdEditorUtility.SetGuidToProperty(serializedProperty, guid);
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
