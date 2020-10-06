using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    // TODO: Use this somehow
    public class GenericJsonValidationService : IGenericJsonValidationService
    {
        public async Task<ValidateObjectResult> ValidateJson<TDeserializedType>(PackObjectResult packObjectResult)
        {
            var json = Encoding.Default.GetString(packObjectResult.File);

            var result = new ValidateObjectResult { LogLevel = LogLevel.Info };
            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<TDeserializedType>(json);
                JsonConvert.SerializeObject(deserializedObject);
                result.IsValid = true;
                result.Message = $"Succesfully validated {packObjectResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Validation for {packObjectResult.Location} failed.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ValidateObjectResult> ValidateJson<TDeserializedType>(string json)
        {
            var result = new ValidateObjectResult { IsValid = true };
            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<TDeserializedType>(json);
                JsonConvert.SerializeObject(deserializedObject);
                result.Message = $"Succesfully validated {json}";
                result.LogLevel = LogLevel.Info;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Message = $"Validation for {json} failed.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }
    }
}
