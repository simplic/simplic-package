using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    // TODO: Use this somehow
    public abstract class ValidateObjectServiceJsonBase<T> : IValidateObjectService where T : class, IContent
    {
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            var json = Encoding.Default.GetString(packObjectResult.File);

            var result = new ValidateObjectResult { LogLevel = LogLevel.Info };
            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<T>(json);
                JsonConvert.SerializeObject(deserializedObject);
                result.IsValid = true;
                result.Message = $"Succesfully validated {ElementName} at {packObjectResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Validation for {ElementName} at {packObjectResult.Location} failed.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }
        private string ElementName => typeof(T).Name;
    }
}
