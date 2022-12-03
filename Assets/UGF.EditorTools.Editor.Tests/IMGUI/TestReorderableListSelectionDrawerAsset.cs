using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/TestReorderableListSelectionDrawerAsset")]
    public class TestReorderableListSelectionDrawerAsset : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> m_objects = new List<ScriptableObject>();
        [AssetId(typeof(ScriptableObject))]
        [SerializeField] private List<GlobalId> m_ids = new List<GlobalId>();

        public List<ScriptableObject> Objects { get { return m_objects; } }
        public List<GlobalId> Ids { get { return m_ids; } }
    }

    [CustomEditor(typeof(TestReorderableListSelectionDrawerAsset), true)]
    public class TestReorderableListSelectionDrawerAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listObjects;
        private ReorderableListSelectionDrawer m_listObjectsSelection;
        private ReorderableListDrawer m_listIds;
        private ReorderableListSelectionDrawerByElementGlobalId m_listIdsSelection;

        private void OnEnable()
        {
            m_listObjects = new ReorderableListDrawer(serializedObject.FindProperty("m_objects"));
            m_listObjectsSelection = new ReorderableListSelectionDrawerByElement(m_listObjects);
            m_listIds = new ReorderableListDrawer(serializedObject.FindProperty("m_ids"));
            m_listIdsSelection = new ReorderableListSelectionDrawerByElementGlobalId(m_listIds);

            m_listObjects.Enable();
            m_listObjectsSelection.Enable();
            m_listIds.Enable();
            m_listIdsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listObjects.Disable();
            m_listObjectsSelection.Disable();
            m_listIds.Disable();
            m_listIdsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listObjects.DrawGUILayout();
                m_listIds.DrawGUILayout();

                m_listObjectsSelection.DrawGUILayout();
                m_listIdsSelection.DrawGUILayout();
            }
        }
    }
}
