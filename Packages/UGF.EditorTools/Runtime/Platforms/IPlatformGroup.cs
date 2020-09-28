namespace UGF.EditorTools.Runtime.Platforms
{
    public interface IPlatformGroup
    {
        object Target { get; }
        IPlatformSettings Settings { get; }
    }
}
