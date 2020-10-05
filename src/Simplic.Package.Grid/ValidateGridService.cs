using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Grid
{
    public class ValidateGridService : IValidateObjectService
    {
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            var result = new ValidateObjectResult { IsValid = true };
            try
            {
                var json = Encoding.Default.GetString(packObjectResult.File);
                var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);
                JsonConvert.SerializeObject(deserializedGrid);
                result.Message = $"Succesfully validated {packObjectResult.Location}";
                result.LogLevel = LogLevel.Info;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Message = $"Validation for {packObjectResult.Location} failed.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }
    }
}