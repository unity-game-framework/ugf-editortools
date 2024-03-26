using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI.AssetReferences
{
    [CreateAssetMenu(menuName = "Tests/TestAssetReferenceAsset")]
    public class TestAssetReferenceAsset : ScriptableObject
    {
        [SerializeField] private AssetReference<ScriptableObject> m_scriptable;
        [SerializeField] private AssetReference<Material> m_material;
        [SerializeField] private List<AssetReference<Material>> m_list = new List<AssetReference<Material>>();
        [SerializeField] private List<AssetReference<Material>> m_list2 = new List<AssetReference<Material>>();

        public AssetReference<ScriptableObject> Scriptable { get { return m_scriptable; } set { m_scriptable = value; } }
        public AssetReference<Material> Material { get { return m_material; } set { m_material = value; } }
        public List<AssetReference<Material>> List { get { return m_list; } }
        public List<AssetReference<Material>> List2 { get { return m_list2; } }
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TestAssetReferenceAsset), true)]
    public class TestAssetReferenceAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScriptable;
        private SerializedProperty m_propertyMaterial;
        private AssetReferenceListDrawer m_list;
        private ReorderableListDrawer m_list2;
        private ReorderableListSelectionDrawerByPath m_listSelection;

        private void OnEnable()
        {
            m_propertyScriptable = serializedObject.FindProperty("m_scriptable");
            m_propertyMaterial = serializedObject.FindProperty("m_material");

            m_list = new AssetReferenceListDrawer(serializedObject.FindProperty("m_list"));
            m_list.Enable();

            m_list2 = new ReorderableListDrawer(serializedObject.FindProperty("m_list2"))
            {
                DisplayAsSingleLine = true
            };

            m_list2.Enable();

            m_listSelection = new ReorderableListSelectionDrawerByPath(m_list, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listSelection.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
            m_list2.Disable();
            m_listSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(m_propertyScriptable);
            EditorGUILayout.PropertyField(m_propertyMaterial);

            m_list.DrawGUILayout();
            m_list2.DrawGUILayout();
            m_listSelection.DrawGUILayout();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
