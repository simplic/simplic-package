namespace Simplic.Package
{
    // TODO: Think of better name.
    // This class holds data to be used for unpacking. It is the raw read content of a file in bytes and more information
    public class UnpackObjectResult
    {
        public byte[] Data { get; set; }
        public string Location { get; set; }
        public MigrationMode Mode { get; set; }
    }
}