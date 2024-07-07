using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdObjectFieldElement : ReferenceGuidObjectFieldElement
    {
        public GlobalId ReferenceValueId { get { return !string.IsNullOrEmpty(ReferenceValueGuid) && GlobalId.TryParse(ReferenceValueGuid, out GlobalId id) ? id : GlobalId.Empty; } set { ReferenceValueGuid = value.ToString(); } }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                ReferenceValueId = GlobalIdEditorUtility.GetGlobalIdFromProperty(serializedProperty);
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            GlobalIdEditorUtility.SetGlobalIdToProperty(serializedProperty, ReferenceValueId);
        }
    }
}
