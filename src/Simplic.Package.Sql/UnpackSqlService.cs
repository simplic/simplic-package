using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    public class UnpackSqlService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var unpackObjectResult = new UnpackObjectResult();

            try
            {
                var content = new SqlContent
                {
                    Data = Encoding.Default.GetString(extractArchiveEntryResult.Data)
                };

                unpackObjectResult.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location, // TODO:
                    Mode = extractArchiveEntryResult.Mode
                };
                unpackObjectResult.LogMessage = $"Succesfully unpacked sql file at {extractArchiveEntryResult.Location}";
                unpackObjectResult.LogLevel = LogLevel.Info;
            }
            catch (Exception ex)
            {
                unpackObjectResult.LogMessage = $"Couldent unpack sql file at {extractArchiveEntryResult.Location}";
                unpackObjectResult.LogLevel = LogLevel.Error;
                unpackObjectResult.Exception = ex;
            }

            return unpackObjectResult;
        }
    }
}