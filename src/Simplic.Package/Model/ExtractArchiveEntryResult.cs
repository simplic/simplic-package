namespace Simplic.Package
{
    // This class holds data to be used for unpacking. It is the raw read content of a file in bytes and more information
    public class ExtractArchiveEntryResult
    {
        public byte[] Data { get; set; }
        public string Location { get; set; }
        public InstallMode Mode { get; set; }
    }
}