using Simplic.Package.Model;

namespace Simplic.Package
{
    public interface IDeserializeObjectService
    {
        IDeserializedContent DeserializeObject(UnpackObjectResult unpackObjectResult);
    }
}
