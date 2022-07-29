using System.Collections.Generic;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Runtime.Assets;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.Assets
{
    [CreateAssetMenu(menuName = "Tests/TestAssetIdReferenceAsset")]
    public class TestAssetIdReferenceAsset : ScriptableObject
    {
        [SerializeField] private AssetIdReference<ScriptableObject> m_scriptable;
        [SerializeField] private AssetIdReference<Material> m_material;
        [SerializeField] private List<AssetIdReference<Material>> m_list = new List<AssetIdReference<Material>>();

        public AssetIdReference<ScriptableObject> Scriptable { get { return m_scriptable; } set { m_scriptable = value; } }
        public AssetIdReference<Material> Material { get { return m_material; } set { m_material = value; } }
        public List<AssetIdReference<Material>> List { get { return m_list; } }
    }

    [CustomEditor(typeof(TestAssetIdReferenceAsset), true)]
    public class TestAssetIdReferenceAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScriptable;
        private SerializedProperty m_propertyMaterial;
        private AssetIdReferenceListDrawer m_list;

        private void OnEnable()
        {
            m_propertyScriptable = serializedObject.FindProperty("m_scriptable");
            m_propertyMaterial = serializedObject.FindProperty("m_material");

            m_list = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_list"));
            m_list.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(m_propertyScriptable);
            EditorGUILayout.PropertyField(m_propertyMaterial);

            m_list.DrawGUILayout();
            m_list.DrawReplaceControlsLayout();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
