using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IValidateObjectService
    {
        Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult);
        // Task<ValidateObjectResult> ValidateMigration(InstallableObject installableObject);
    }
}
