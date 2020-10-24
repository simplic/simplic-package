using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for validating the PackageConfiguration of a package
    /// </summary>
    public interface IValidatePackageConfigurationService
    {
        /// <summary>
        /// Validates a given PackageConfiguration object
        /// </summary>
        /// <param name="packageConfiguration">The PackageConfiguration object to validate</param>
        /// <returns>A PackageConfigurationResult object</returns>
        Task<ValidatePackageConfigurationResult> Validate(PackageConfiguration packageConfiguration);
    }
}