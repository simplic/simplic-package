namespace Simplic.Package
{
    public interface IDeserializeObjectService
    {
        IDeserializedContent DeserializeObject(byte[] entryBytes);
    }
}
