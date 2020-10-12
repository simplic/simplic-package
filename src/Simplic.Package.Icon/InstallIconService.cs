using Simplic.Icon;
using System;
using System.Linq;
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
            if (installableObject.Content is IconContent icon)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info,
                };

                try
                {
                    var simplicIcon = new Simplic.Icon.Icon
                    {
                        IconBlob = icon.Blob,
                        Name = icon.Name
                    };

                    var guid = GetGuidByName(icon.Name);
                    if (guid != Guid.Empty)
                        simplicIcon.Guid = guid;
                    else
                        simplicIcon.Guid = Guid.NewGuid();

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

        public Guid GetGuidByName(string name)
        {
            var icon = iconService.GetAll().FirstOrDefault(x => x.Name == name);
            
            if (icon != null)
                return icon.Guid;
            else
                return Guid.Empty;
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