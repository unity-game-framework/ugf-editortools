using System;
using UGF.EditorTools.Runtime.Attributes;

namespace UGF.EditorTools.Runtime.Assets
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AssetIdReferenceListAttribute : ListAttribute
    {
        public bool DisplayAsReplace { get; }
        public bool DisplayReplaceButton { get; }

        public AssetIdReferenceListAttribute(bool displayAsReplace = true, bool displayReplaceButton = true)
        {
            DisplayAsReplace = displayAsReplace;
            DisplayReplaceButton = displayReplaceButton;
        }
    }
}
