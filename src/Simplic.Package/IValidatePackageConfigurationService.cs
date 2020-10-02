using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IValidatePackageConfigurationService
    {
        Task<ValidatePackageConfigurationResult> Validate(PackageConfiguration packageConfiguration);
    }
}