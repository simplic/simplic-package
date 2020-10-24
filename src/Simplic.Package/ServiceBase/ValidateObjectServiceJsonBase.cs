using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Base ValidateObjectService implementation for json objects
    /// Checks whether the json file can be deserialized and serialized again
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    public abstract class ValidateObjectServiceJsonBase<T> : IValidateObjectService where T : class, IContent
    {
        /// <summary>
        /// Checks whether the json file can be deserialized and serialized again
        /// </summary>
        /// <param name="packObjectResult">The packObjectResult containing the json to check</param>
        /// <returns>A ValidateObjectResult object</returns>
        public async Task<ValidateObjectResult> Validate(PackObjectResult packObjectResult)
        {
            var json = Encoding.Default.GetString(packObjectResult.File);

            var result = new ValidateObjectResult { LogLevel = LogLevel.Info };
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                var deserializedObject = JsonConvert.DeserializeObject<T>(json, serializerSettings);
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
