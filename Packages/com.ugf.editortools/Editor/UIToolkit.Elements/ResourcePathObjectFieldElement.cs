using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ResourcePathObjectFieldElement : PropertyBindObjectFieldElement
    {
        public string ResourcePath
        {
            get { return m_resourcePath; }
            set
            {
                Object asset = Resources.Load(value, objectType);

                if (!string.IsNullOrEmpty(value) && asset == null)
                {
                    asset = EditorIMGUIUtility.MissingObject;
                }

                m_resourcePath = value;

                this.value = asset;
            }
        }

        private string m_resourcePath = string.Empty;

        public ResourcePathObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : base(serializedProperty, field)
        {
            allowSceneObjects = false;
        }

        public ResourcePathObjectFieldElement()
        {
            allowSceneObjects = false;
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (!EditorIMGUIUtility.IsMissingObject(newValue))
            {
                m_resourcePath = newValue != null && AssetsEditorUtility.TryGetResourcesPath(newValue, out string result) ? result : string.Empty;
            }
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                ResourcePath = serializedProperty.stringValue;
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            serializedProperty.stringValue = ResourcePath;
        }
    }
}
