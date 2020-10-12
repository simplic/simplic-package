using Simplic.Icon;
using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Icon
{
    public class InstallIconService : IInstallObjectService
    {
        private readonly IIconService iconService;
        public InstallIconService(IIconService iconService)
        {
            this.iconService = iconService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedIcon icon)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info,
                };

                try
                {
                    var simplicIcon = new Simplic.Icon.Icon
                    {
                        Guid = icon.Id,
                        IconBlob = icon.Blob,
                        Name = icon.Name,
                    };

                    result.Success = iconService.Save(simplicIcon);
                    if (result.Success)
                        result.Message = $"Installed icon at {installableObject.Target}.";
                    else
                    {
                        result.Message = $"Failed to install icon at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Warning;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install icon at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }

                return result;
            }
            throw new InvalidContentException();
        }

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}