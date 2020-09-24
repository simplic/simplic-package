using Simplic.Package;

namespace Simplic.Package
{
    public interface IUnpackObjectService
    {
        InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult);
        InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult);
    }
}
