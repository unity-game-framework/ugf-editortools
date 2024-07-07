using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.EditorTools.Editor.UIToolkit.Elements
{
    public class ReferenceGuidObjectFieldElement : ReferenceObjectFieldElement
    {
        public string ReferenceValueGuid
        {
            get { return m_referenceValueGuid; }
            set
            {
                string path = AssetDatabase.GUIDToAssetPath(value);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                if (!string.IsNullOrEmpty(path) && asset == null)
                {
                    asset = EditorIMGUIUtility.MissingObject;
                }

                m_referenceValueGuid = value;

                this.value = asset;
            }
        }

        private string m_referenceValueGuid = string.Empty;

        public ReferenceGuidObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : base(serializedProperty, field)
        {
            ReferenceTooltipLabel = "Guid";
        }

        public ReferenceGuidObjectFieldElement()
        {
            ReferenceTooltipLabel = "Guid";
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (!EditorIMGUIUtility.IsMissingObject(newValue))
            {
                m_referenceValueGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(newValue));
            }
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                ReferenceValueGuid = serializedProperty.stringValue;
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            serializedProperty.stringValue = ReferenceValueGuid;
        }

        protected override string OnGetReferenceValueDisplay(SerializedProperty serializedProperty)
        {
            return ReferenceValueGuid;
        }
    }
}
