using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class ValidateSqlService : IValidateObjectService
    {
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            return new ValidateObjectResult
            {
                IsOkay = true
            };
        }
    }
}
