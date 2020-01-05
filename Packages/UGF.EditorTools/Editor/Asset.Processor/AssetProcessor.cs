namespace UGF.EditorTools.Editor.Asset.Processor
{
    public abstract class AssetProcessor : IAssetProcessor
    {
        public int Order { get; set; }

        public virtual void OnImport(string path)
        {
        }

        public virtual void OnDelete(string path)
        {
        }

        public virtual void OnMoved(string pathFrom, string pathTo)
        {
        }
    }
}
