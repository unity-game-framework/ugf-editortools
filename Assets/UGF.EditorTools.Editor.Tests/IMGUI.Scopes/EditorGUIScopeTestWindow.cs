using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Scopes
{
    public class EditorGUIScopeTestWindow : EditorWindow
    {
        [MenuItem("Tests/EditorGUIScopeTestWindow")]
        private static void Menu()
        {
            GetWindow<EditorGUIScopeTestWindow>();
        }

        private void OnGUI()
        {
            EditorGUILayout.IntField("Int Field", 0);

            using (new IndentLevelScope(1))
            {
                EditorGUILayout.IntField("Int Field Indent 1", 0);
            }

            using (new IndentLevelScope(5))
            {
                EditorGUILayout.IntField("Int Field Indent 5", 0);
            }

            using (new IndentIncrementScope(1))
            {
                EditorGUILayout.IntField("Int Field Indent 1", 0);

                using (new IndentIncrementScope(1))
                {
                    EditorGUILayout.IntField("Int Field Indent 2", 0);
                }
            }

            using (new GUIColorScope(Color.magenta))
            {
                EditorGUILayout.IntField("Int Field Colored", 0);
            }

            using (new GUIBackgroundColorScope(Color.green))
            {
                EditorGUILayout.IntField("Int Field Colored", 0);
            }

            EditorGUILayout.IntField("Int Field", 0);
        }
    }
}
