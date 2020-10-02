using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class ValidateGridService : IValidateObjectService
    {
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            var result = new ValidateObjectResult { IsOkay = true };
            try
            {
                var json = Encoding.Default.GetString(packObjectResult.File);
                var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);
                JsonConvert.SerializeObject(deserializedGrid);
                result.LogMessage = $"Succesfully validated {packObjectResult.Location}";
                result.LogLevel = LogLevel.Info;
            }
            catch (Exception ex)
            {
                result.IsOkay = false;
                result.LogMessage = $"Validation for {packObjectResult.Location} failed with Exception {ex}";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }
    }
}