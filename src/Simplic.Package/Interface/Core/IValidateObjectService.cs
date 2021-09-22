using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for validating objects.
    /// </summary>
    public interface IValidateObjectService
    {
        /// <summary>
        /// Validates a given object.
        /// </summary>
        /// <param name="packObjectResult">The PackObjectResult whose File to validate.</param>
        /// <returns>A <see cref="ValidateObjectResult"/> object.</returns>
        Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult);
    }
}