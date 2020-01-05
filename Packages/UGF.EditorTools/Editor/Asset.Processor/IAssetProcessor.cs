namespace UGF.EditorTools.Editor.Asset.Processor
{
    public interface IAssetProcessor
    {
        int Order { get; }

        void OnImport(string path);
        void OnDelete(string path);
        void OnMoved(string pathFrom, string pathTo);
    }
}
