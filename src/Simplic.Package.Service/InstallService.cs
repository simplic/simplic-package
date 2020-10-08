using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class InstallService : IInstallService
    {
        private readonly IUnityContainer container;
        private readonly ILogService logService;
        private readonly IPackageTrackingRepository packageTrackingRepository;

        public InstallService(IUnityContainer container, ILogService logService, IPackageTrackingRepository packageTrackingRepository)
        {
            this.container = container;
            this.logService = logService;
            this.packageTrackingRepository = packageTrackingRepository;
        }

        /// <summary>
        /// Installs a package
        /// </summary>
        /// <param name="package">The package to install</param>
        public async Task Install(Package package)
        {
            //Debugger.Launch();

            // Check dependencies
            var checkDependencyService = container.Resolve<ICheckDependencyService>();

            var checkDependenciesResult = await checkDependencyService.CheckAllDependencies(package.Dependencies);
            if (checkDependenciesResult.LogLevel != LogLevel.Error)
                await logService.WriteAsync(checkDependenciesResult.Message, checkDependenciesResult.LogLevel);
            else
                throw new MissingDependencyException(checkDependenciesResult.Message);
            
            // Check if package already exists and act accordingly
            var existingPackageVersion = await packageTrackingRepository.GetPackageVersion(package.Guid);
            if (package.Version == existingPackageVersion) { }
            //throw new ExistingPackageException($"The version {existingPackageVersion} of this package is already installed.");
            else if (package.Version < existingPackageVersion)
            {
                // await logService.WriteAsync($"A later version ({existingPackageVersion}) of {package.Name} is already installed.", LogLevel.Info);
                throw new ExistingPackageException($"A later version ({existingPackageVersion}) of this package is already installed.");
            }
            await logService.WriteAsync($"Found no installation of version {package.Version} of this package. Proceeding to install package.", LogLevel.Info);

            // Install the objects
            foreach (var item in package.UnpackedObjects)
            {
                var installObjectService = container.Resolve<IInstallObjectService>(item.Key); // TODO: Exception handeling ?

                foreach (var installableObject in item.Value)
                {
                    var install = installableObject.Mode == InstallMode.Deploy;
                    if (installableObject.Mode == InstallMode.Migrate)
                    {
                        var checkMigrationResult = await installObjectService.CheckMigration(installableObject);
                        install = checkMigrationResult.CanMigrate;
                        await logService.WriteAsync(checkMigrationResult.Message, checkMigrationResult.LogLevel);
                    }

                    if (install)
                    {
                        var installObjectResult = await installObjectService.InstallObject(installableObject);

                        if (!installObjectResult.Success)
                            throw new InvalidObjectException(installObjectResult.Message, installObjectResult.Exception);
                        else
                            await logService.WriteAsync(installObjectResult.Message, installObjectResult.LogLevel);
                    }
                }
            }

            // Add to InstalledPackages in database
            var success = await packageTrackingRepository.AddPackgageVersion(package);
            if (success)
                await logService.WriteAsync($"Wrote package with name {package.Name} and version {package.Version} to installed packages table", LogLevel.Info);
            else
                await logService.WriteAsync($"Failed to write package with name {package.Name} and version {package.Version} to installed packages table.", LogLevel.Error);
        }

        public async Task Uninstall(Package unpackedPackage)
        {
            throw new NotImplementedException();
        }

        public async Task Overwrite(Package unpackedPackage)
        {
            throw new NotImplementedException();
        }
    }
}