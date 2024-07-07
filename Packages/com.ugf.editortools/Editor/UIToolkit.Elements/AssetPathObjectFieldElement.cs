using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class AssetPathObjectFieldElement : PropertyBindObjectFieldElement
    {
        public string AssetPath
        {
            get { return m_assetPath; }
            set
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(value);

                if (!string.IsNullOrEmpty(value) && asset == null)
                {
                    asset = EditorIMGUIUtility.MissingObject;
                }

                m_assetPath = value;

                this.value = asset;
            }
        }

        private string m_assetPath = string.Empty;

        public AssetPathObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : base(serializedProperty, field)
        {
            allowSceneObjects = false;
        }

        public AssetPathObjectFieldElement()
        {
            allowSceneObjects = false;
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (!EditorIMGUIUtility.IsMissingObject(newValue))
            {
                m_assetPath = AssetDatabase.GetAssetPath(newValue);
            }
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                AssetPath = serializedProperty.stringValue;
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            serializedProperty.stringValue = AssetPath;
        }
    }
}
