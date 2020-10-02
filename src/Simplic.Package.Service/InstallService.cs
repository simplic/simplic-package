using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="unpackedPackage">The package to install</param>
        public async Task Install(Package unpackedPackage)
        {
            // Debugger.Launch();
            // Check for dependencies
            var checkDependencyService = container.Resolve<ICheckDependencyService>();

            var checkDependencyResults = new List<CheckDependencyResult>();
            foreach (var dependency in unpackedPackage.Dependencies)
                checkDependencyResults.Add(await checkDependencyService.Check(dependency));

            var missingDependencyResults = checkDependencyResults.Where(x => !x.Exists); // Give these as output

            var first = missingDependencyResults.FirstOrDefault();
            if (first != null)
                throw new MissingDependencyException($"{first.Dependency.PackageName} with Version {first.Dependency.Version} doesnt exist. Latest found Version: {first.LatestExistingVersion}");

            // Check if package already exists and act accordingly
            var existingPackageVersion = await packageTrackingRepository.GetLatestPackageVersion(unpackedPackage.Name);
            if (unpackedPackage?.Version == existingPackageVersion)
            {
                await logService.WriteAsync($"Version {existingPackageVersion} of {unpackedPackage.Name} is already installed.", LogLevel.Error);
                // throw new ExistingPackageException($"The version {existingPackageVersion} of this package is already installed.");
            }
            else if (unpackedPackage?.Version < existingPackageVersion)
            {
                await logService.WriteAsync($"A later version ({existingPackageVersion}) of {unpackedPackage.Name} is already installed.", LogLevel.Info);
                // throw new ExistingPackageException($"A later version ({existingPackageVersion}) of the package is already installed.");
            }

            // Install the objects
            foreach (var item in unpackedPackage.UnpackedObjects)
            {
                var installService = container.Resolve<IInstallObjectService>(item.Key); // TODO: Exception handeling

                foreach (var installableObject in item.Value)
                {
                    var install = installableObject.Mode == InstallMode.Deploy;
                    if (installableObject.Mode == InstallMode.Migrate)
                    {
                        var checkMigrationResult = await installService.CheckMigration(installableObject);
                        install = checkMigrationResult.CanMigrate;
                        await logService.WriteAsync(checkMigrationResult.LogMessage, checkMigrationResult.LogLevel);
                    }

                    if (install)
                    {
                        var installObjectResult = await installService.InstallObject(installableObject);
                        await logService.WriteAsync(installObjectResult.LogMessage, installObjectResult.LogLevel);

                        if (!installObjectResult.Success)
                            throw new InvalidObjectException(installObjectResult.LogMessage, installObjectResult.Exception);
                    }
                }
            }

            // Add to InstalledPackages in database
            var success = await packageTrackingRepository.AddPackgageVersion(unpackedPackage.Name, unpackedPackage.Guid, unpackedPackage.Version);
            if (success)
                await logService.WriteAsync($"Wrote package with name {unpackedPackage.Name} and version {unpackedPackage.Version} to installed packages table", LogLevel.Info);
            else
                await logService.WriteAsync($"Failed to write package with name {unpackedPackage.Name} and version {unpackedPackage.Version} to installed packages table.", LogLevel.Error);
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