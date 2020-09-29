using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class ValidateGridService : IValidateObjectService
    {
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            var result = new ValidateObjectResult
            {
                IsOkay = true
            };

            try
            {
                var json = Encoding.Default.GetString(packObjectResult.File);
                var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);
                JsonConvert.SerializeObject(deserializedGrid);
            } 
            catch (Exception ex)
            {
                result.Exception = ex;
                result.ErrorMessage = $"Couldent serialize or deserialize the given grid file: {packObjectResult.Location}";
                result.IsOkay = false;
            }
            return result;
        }
    }
}
