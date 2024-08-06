using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class AssetGuidObjectFieldElement : PropertyBindObjectFieldElement
    {
        public string AssetGuid
        {
            get { return m_assetGuid; }
            set
            {
                string path = AssetDatabase.GUIDToAssetPath(value);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                if (!string.IsNullOrEmpty(path) && asset == null)
                {
                    asset = EditorIMGUIUtility.MissingObject;
                }

                m_assetGuid = value;

                this.value = asset;
            }
        }

        private string m_assetGuid = string.Empty;

        public AssetGuidObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : base(serializedProperty, field)
        {
            allowSceneObjects = false;
        }

        public AssetGuidObjectFieldElement()
        {
            allowSceneObjects = false;
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (!EditorIMGUIUtility.IsMissingObject(newValue))
            {
                m_assetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(newValue));
            }
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                AssetGuid = serializedProperty.stringValue;
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            serializedProperty.stringValue = AssetGuid;
        }
    }
}
