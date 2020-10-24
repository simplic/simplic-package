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
        private readonly ILogService logService;

        public InstallIconService(IIconService iconService, ILogService logService)
        {
            this.iconService = iconService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is IconContent icon)
            {
                var result = new InstallObjectResult { Success = true };

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

                    var execResult = iconService.Save(simplicIcon);

                    if (execResult)
                    {
                        await logService.WriteAsync($"Installed icon at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install icon at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install icon at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
                }

                return result;
            }
            throw new InvalidContentException();
        }

        public Guid GetGuidByName(string name)
        {
            // TODO: Make this more efficient
            var icon = iconService.GetAll().FirstOrDefault(x => x.Name == name);
            
            if (icon != null)
                return icon.Guid;
            else
                return Guid.Empty;
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}