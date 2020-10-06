using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IGenericJsonValidationService
    {
        Task<ValidateObjectResult> ValidateJson<TDeserializedType>(PackObjectResult packObjectResult);
        Task<ValidateObjectResult> ValidateJson<TDeserializedType>(string json);
    }
}
