using System;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.Scopes
{
    public class InspectorGUIScopeTestWindow : EditorWindow
    {
        [MenuItem("Tests/InspectorGUIScopeTestWindow")]
        private static void Menu()
        {
            CreateWindow<InspectorGUIScopeTestWindow>();
        }

        private void OnGUI()
        {
            EditorGUILayout.Vector2Field("Vector 2", Vector2.zero);
            EditorGUILayout.Vector4Field("Vector 4", Vector4.zero);
            EditorGUILayout.Foldout(true, "Foldout");

            EditorGUILayout.LabelField("Scope Open", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            using (new InspectorGUIScope(true))
            {
                EditorGUILayout.Vector2Field("Vector 2 Scoped", Vector2.zero);
                EditorGUILayout.Vector4Field("Vector 4 Scoped", Vector4.zero);
                EditorGUILayout.Foldout(true, "Foldout Scoped");
            }

            using (new EditorGUI.IndentLevelScope())
            {
                using (new InspectorGUIScope(true))
                {
                    EditorGUILayout.Vector2Field("Vector 2 Scoped", Vector2.zero);
                    EditorGUILayout.Vector4Field("Vector 4 Scoped", Vector4.zero);
                    EditorGUILayout.Foldout(true, "Foldout Scoped");
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scope Close", EditorStyles.boldLabel);

            EditorGUILayout.Vector2Field("Vector 2", Vector2.zero);
            EditorGUILayout.Vector4Field("Vector 4", Vector4.zero);
            EditorGUILayout.Foldout(true, "Foldout");
        }
    }
}
