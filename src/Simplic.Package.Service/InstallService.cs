using System;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Exceptions;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IInstallService"/>
    public class InstallService : IInstallService
    {
        private readonly IUnityContainer container;
        private readonly ILogService logService;
        private readonly IPackageTrackingRepository packageTrackingRepository;
        private readonly IMigrationService migrationService;
        private readonly IExtensionService extensionService;

        /// <summary>
        /// Initialize a new instance of <see cref="InstallService"/>.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="logService"></param>
        /// <param name="packageTrackingRepository"></param>
        /// <param name="migrationService"></param>
        public InstallService(
            IUnityContainer container,
            ILogService logService,
            IPackageTrackingRepository packageTrackingRepository,
            IMigrationService migrationService,
            IExtensionService extensionService)
        {
            this.container = container;
            this.logService = logService;
            this.packageTrackingRepository = packageTrackingRepository;
            this.migrationService = migrationService;
            this.extensionService = extensionService;
        }

        /// <summary>
        /// Installs a package
        /// </summary>
        /// <param name="package">The package to install</param>
        public async Task Install(Package package)
        {
            // Check dependencies
            var checkDependencyService = container.Resolve<ICheckDependencyService>();

            var checkDependenciesResult = await checkDependencyService.CheckDependencies(package.Dependencies);
            if (checkDependenciesResult.LogLevel != LogLevel.Error)
                await logService.WriteAsync(checkDependenciesResult.Message, checkDependenciesResult.LogLevel);
            else
                throw new MissingDependencyException(checkDependenciesResult.Message);

            // Check if package already exists and act accordingly
            var existingPackageVersion = await packageTrackingRepository.GetPackageVersion(package.Guid);

            if (package.Version == existingPackageVersion)
            {
                throw new ExistingPackageException($"The version {existingPackageVersion}" +
                    $" of this package is already installed.");
            }

            else if (package.Version < existingPackageVersion)
            {
                await logService.WriteAsync($"A later version ({existingPackageVersion}) " +
                    $"of {package.Name} is already installed.", LogLevel.Info);

                throw new ExistingPackageException($"A later version ({existingPackageVersion})" +
                    $" of this package is already installed.");
            }

            await logService.WriteAsync($"Found no installation of version {package.Version} of this package. " +
                $"Proceeding to install package.", LogLevel.Info);

            // Load extensions.
            if (package.Extensions.Any())
            {
                await logService.WriteAsync("Loading extensions...", LogLevel.Info);
                extensionService.LoadExtensions(package.Extensions);

                if (package.Extensions.Any(x => !ExtensionHelper.LoadedExtensions.Contains(x)))
                {
                    await logService.WriteAsync("Could not load all extensions", LogLevel.Error);
                    throw new MissingExtensionException($"Could not load " +
                        $"{package.Extensions.First(x => !ExtensionHelper.LoadedExtensions.Contains(x))}");
                }
            }

            // Install the objects
            foreach (var item in package.UnpackedObjects)
            {
                var installObjectService = container.Resolve<IInstallObjectService>(item.Key); // TODO: Exception handeling ?

                // Request values.
                try
                {
                    var requestValueService = container.Resolve<IRequestValueService>(item.Key);
                    if (requestValueService != null)
                    {
                        var res = requestValueService.RequestValue(item.Value);
                        if (!res.Success)
                        {
                            await logService.WriteAsync($"Could not request all values for {item.Key}", LogLevel.Error);
                        }
                    }
                }
                catch (ResolutionFailedException)
                {
                    // pass (no value request service registered).
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Error while requestion value for {item.Key}", LogLevel.Error, ex);
                    throw;
                }

                // Installation of all objects.
                foreach (var installableObject in item.Value)
                {
                    var install = installableObject.Mode == InstallMode.Deploy;
                    if (installableObject.Mode == InstallMode.Migrate)
                    {
                        var checkMigrationResult = await migrationService.CheckMigration(installableObject);
                        install = checkMigrationResult.CanMigrate;
                        await logService.WriteAsync(checkMigrationResult.Message, checkMigrationResult.LogLevel);
                    }

                    if (install)
                    {
                        var installObjectResult = await installObjectService.InstallObject(installableObject);

                        if (!installObjectResult.Success)
                            throw new InvalidObjectException();
                    }
                }
            }

            // Add to InstalledPackages in database
            var success = await packageTrackingRepository.AddPackgageVersion(package);
            if (success)
                await logService.WriteAsync($"Wrote package with name {package.Name} " +
                    $"and version {package.Version} to installed packages table", LogLevel.Info);
            else
                await logService.WriteAsync($"Failed to write package with name {package.Name}" +
                    $" and version {package.Version} to installed packages table.", LogLevel.Error);
        }

        /// <inheritdoc/>
        public async Task Uninstall(Package unpackedPackage)
        {
            throw new NotImplementedException();
        }
    }
}