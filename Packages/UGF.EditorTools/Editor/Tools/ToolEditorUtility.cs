using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tools
{
    public static class ToolEditorUtility
    {
        public static GUIContent EditPositionContent { get { return new GUIContent(EditorGUIUtility.IconContent("MoveTool").image, "Edit position."); } }
        public static GUIContent EditShapeContent { get { return new GUIContent(EditorGUIUtility.IconContent("EditCollider").image, "Edit shape."); } }
    }
}
