namespace Simplic.Package
{
    public interface IUnpackObjectService
    {
        InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult, bool deserialize);
        InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult);
        InstallableObject CopyObject(UnpackObjectResult unpackObjectResult);
    }
}