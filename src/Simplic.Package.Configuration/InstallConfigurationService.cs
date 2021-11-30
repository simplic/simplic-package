using Simplic.Configuration;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Service to install configurations.
    /// </summary>
    public class InstallConfigurationService : IInstallObjectService
    {
        private readonly IConfigurationService configurationService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instace of <see cref="InstallConfigurationService"/>.
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="logService"></param>
        public InstallConfigurationService(IConfigurationService configurationService, ILogService logService)
        {
            this.configurationService = configurationService;
            this.logService = logService;
        }

        /// <summary>
        /// Installs a configuration.
        /// </summary>
        /// <param name="installableObject">The installable object.</param>
        /// <returns>Whether the installation was successfull.</returns>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (!(installableObject.Content is Configuration))
                return new InstallObjectResult { Success = false };

            var configuration = installableObject.Content as Configuration;

            await logService.WriteAsync($"Installing configuration: {configuration.ConfigurationName}.",
                LogLevel.Info);

            try
            {
                // Installs a configuration where the value is present in the package.
                if (configuration.ValueSource == ConfigurationValueSource.PackageValue)
                {
                    configurationService.SetValue(
                        configuration.ConfigurationName,
                        configuration.PlugInName,
                        configuration.UserName,
                        configuration.Value);

                    await logService.WriteAsync($"Configuration {configuration.ConfigurationName} " +
                        $"successfully installed.", LogLevel.Info);

                    return new InstallObjectResult { Success = true };
                }
                else if (configuration.ValueSource == ConfigurationValueSource.RequestValue)
                {

                    Console.WriteLine($"\n \nConfiguration value needed for: \"{configuration.ConfigurationName}\" " +
                        $"in PlugIn: \"{configuration.PlugInName}\":");

                    configuration.Value = Console.ReadLine().Trim();

                    configurationService.SetValue(
                        configuration.ConfigurationName,
                        configuration.PlugInName,
                        configuration.UserName,
                        configuration.Value);

                    await logService.WriteAsync($"Configuration {configuration.ConfigurationName} " +
                        $"successfully installed.", LogLevel.Info);

                    return new InstallObjectResult { Success = true };
                }
                else
                {
                    await logService.WriteAsync($"Unable to install Configuration: {configuration.ConfigurationName}," +
                        $"no value mode found.", LogLevel.Error);

                    return new InstallObjectResult { Success = false };
                }

            }
            catch (Exception ex)
            {
                await logService.WriteAsync($"Error while installing configuration {configuration.ConfigurationName}.",
                    LogLevel.Error, ex);

                return new InstallObjectResult { Success = false };
            }


        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
