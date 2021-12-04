using System;
using System.Collections.Generic;

namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Implementation of <see cref="IRequestValueService"/> for configurations within a cli application.
    /// </summary>
    public class CliRequestConfigurationValueService : IRequestValueService
    {
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="CliRequestConfigurationValueService"/>.
        /// </summary>
        /// <param name="logService"></param>
        public CliRequestConfigurationValueService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <summary>
        /// Requests the value for all configurations that need a requested value.
        /// </summary>
        /// <param name="installableObjects">A list of installable objects with cofiguration objects as content.</param>
        /// <returns>Wether the installation was successfull.</returns>
        public RequestValueResult RequestValue(IList<InstallableObject> installableObjects)
        {
            try
            {
                foreach (var installableObject in installableObjects)
                {
                    if (installableObject.Content is Configuration configuration)
                    {
                        if (configuration.ValueSource == ConfigurationValueSource.RequestValue)
                        {
                            Console.WriteLine($"\n \nConfiguration value needed for: " +
                                $"\"{configuration.ConfigurationName}\" " +
                                $"in PlugIn: \"{configuration.PlugInName}\":");

                            configuration.Value = Console.ReadLine();
                        }
                    }
                }

                return new RequestValueResult { Success = true };
            }
            catch (Exception ex)
            {
                logService.WriteAsync("Error while requesting value for configurations", LogLevel.Error, ex);
                return new RequestValueResult { Success = false };
            }
        }
    }
}
