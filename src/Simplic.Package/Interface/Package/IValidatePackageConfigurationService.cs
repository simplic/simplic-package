using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for validating the PackageConfiguration of a package.
    /// </summary>
    public interface IValidatePackageConfigurationService
    {
        /// <summary>
        /// Validates a given <see cref="PackageConfiguration"/> object.
        /// </summary>
        /// <param name="packageConfiguration">The <see cref="PackageConfiguration"/> object to validate.</param>
        /// <returns>A <see cref="ValidatePackageConfigurationResult"/> object.</returns>
        Task<ValidatePackageConfigurationResult> Validate(PackageConfiguration packageConfiguration);
    }
}