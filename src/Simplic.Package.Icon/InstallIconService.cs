using Simplic.Icon;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    /// <summary>
    /// Service to install an icon.
    /// </summary>
    public class InstallIconService : IInstallObjectService
    {
        private readonly IIconService iconService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallIconService"/>.
        /// </summary>
        /// <param name="iconService"></param>
        /// <param name="logService"></param>
        public InstallIconService(IIconService iconService, ILogService logService)
        {
            this.iconService = iconService;
            this.logService = logService;
        }

        /// <inheritdoc/>
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

        /// <summary>
        /// Gets the unique identfier of an icon by the icon name.
        /// </summary>
        /// <param name="name">The name of the icon.</param>
        /// <returns>Returns the guid of the icon.</returns>
        public Guid GetGuidByName(string name)
        {
            // TODO: Make this more efficient
            var icon = iconService.GetAll().FirstOrDefault(x => x.Name == name);

            if (icon != null)
                return icon.Guid;
            else
                return Guid.Empty;
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}