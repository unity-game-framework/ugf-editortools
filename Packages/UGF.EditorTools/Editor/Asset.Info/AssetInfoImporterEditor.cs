using System;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using Object = UnityEngine.Object;

namespace UGF.EditorTools.Editor.Asset.Info
{
    [CustomEditor(typeof(AssetInfoImporter), true)]
    public class AssetInfoImporterEditor : ScriptedImporterEditor
    {
        protected override Type extraDataType { get; } = typeof(AssetInfoData);

        private string m_infoName;

        public override void OnEnable()
        {
            base.OnEnable();

            var importer = (AssetInfoImporter)target;

            m_infoName = ObjectNames.NicifyVariableName(importer.InfoType.Name);
        }

        public override void OnInspectorGUI()
        {
            OnDrawImportSettings();
            OnDrawInfo();

            ApplyRevertGUI();
        }

        protected virtual void OnDrawImportSettings()
        {
            EditorIMGUIUtility.DrawDefaultInspector(serializedObject);
        }

        protected virtual void OnDrawInfo()
        {
            extraDataSerializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(m_infoName, EditorStyles.boldLabel);

            EditorIMGUIUtility.DrawSerializedPropertyChildren(extraDataSerializedObject, "m_info");

            extraDataSerializedObject.ApplyModifiedProperties();
        }

        protected override void InitializeExtraDataInstance(Object extraData, int targetIndex)
        {
            var data = (AssetInfoData)extraData;
            var importer = (AssetInfoImporter)targets[targetIndex];
            IAssetInfo info = importer.Load();

            data.Info = info;
        }

        protected override void Apply()
        {
            base.Apply();

            var data = (AssetInfoData)extraDataTarget;
            var importer = (AssetInfoImporter)target;

            importer.Save(data.Info);
        }
    }
}
