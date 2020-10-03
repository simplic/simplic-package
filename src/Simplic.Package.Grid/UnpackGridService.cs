﻿using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Grid
{
    public class UnpackGridService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var unpackObjectResult = new UnpackObjectResult();

            try
            {
                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var content = JsonConvert.DeserializeObject<DeserializedGrid>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error }); // TODO: try catch

                unpackObjectResult.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location, // TODO: What to put here? this could be useful
                    Mode = extractArchiveEntryResult.Mode
                };
                unpackObjectResult.Message = $"Succesfully deserialiazed the grid at {extractArchiveEntryResult.Location}";
                unpackObjectResult.LogLevel = LogLevel.Info;
            }
            catch (JsonSerializationException jse)
            {
                unpackObjectResult.Message = $"Failed to serialize the grid at {extractArchiveEntryResult.Location}";
                unpackObjectResult.LogLevel = LogLevel.Error;
                unpackObjectResult.Exception = jse;
            }

            return unpackObjectResult;
        }
    }
}