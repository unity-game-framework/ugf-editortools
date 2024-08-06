using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.UIToolkit.Elements;
using UGF.EditorTools.Runtime.Ids;
using UnityEditor;

namespace UGF.EditorTools.Editor.Assets
{
    public class AssetIdObjectFieldElement : AssetGuidObjectFieldElement
    {
        public GlobalId AssetId { get { return !string.IsNullOrEmpty(AssetGuid) && GlobalId.TryParse(AssetGuid, out GlobalId id) ? id : GlobalId.Empty; } set { AssetGuid = value.ToString(); } }

        public AssetIdObjectFieldElement(SerializedProperty serializedProperty, bool field = false) : base(serializedProperty, field)
        {
        }

        public AssetIdObjectFieldElement()
        {
        }

        protected override void OnUpdate(SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasMultipleDifferentValues)
            {
                AssetId = GlobalIdEditorUtility.GetGlobalIdFromProperty(serializedProperty);
            }
        }

        protected override void OnApply(SerializedProperty serializedProperty)
        {
            GlobalIdEditorUtility.SetGlobalIdToProperty(serializedProperty, AssetId);
        }
    }
}
