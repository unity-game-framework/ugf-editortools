using System.Collections.Generic;

namespace UGF.EditorTools.Editor.Asset.Processor
{
    public class AssetProcessorComparer : IComparer<IAssetProcessor>
    {
        public static IComparer<IAssetProcessor> Default { get; } = new AssetProcessorComparer();

        public int Compare(IAssetProcessor x, IAssetProcessor y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            return x.Order.CompareTo(y.Order);
        }
    }
}
