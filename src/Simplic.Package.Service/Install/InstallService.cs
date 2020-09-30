using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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

        public async Task Install(Package unpackedPackage)
        {

            Debugger.Launch();
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
            if (existingPackageVersion != null && existingPackageVersion == unpackedPackage.Version)
            {
                await logService.WriteAsync($"A package with name {unpackedPackage.Name} and version {unpackedPackage.Version} is already installed.", LogLevel.Info);
            }
            else if (existingPackageVersion != null && existingPackageVersion > unpackedPackage.Version)
            {
                await logService.WriteAsync($"A package with name {unpackedPackage.Name} and version {existingPackageVersion} is already installed.", LogLevel.Info);
            }

            // Install the objects
            foreach (var item in unpackedPackage.UnpackedObjects)
            {
                var installService = container.Resolve<IInstallObjectService>(item.Key); // TODO: Exception handeling

                foreach (var installableObject in item.Value)
                {
                    var install = installableObject.Mode == MigrationMode.Deploy;
                    if (installableObject.Mode == MigrationMode.Migrate)
                    {
                        var checkMigrationResult = await installService.CheckMigration(installableObject);
                        install = checkMigrationResult.CanMigrate;
                        await logService.WriteAsync(checkMigrationResult.LogMessage, checkMigrationResult.LogLevel);
                    }

                    if (install) {
                        var installObjectResult = await installService.InstallObject(installableObject);
                        await logService.WriteAsync(installObjectResult.LogMessage, installObjectResult.LogLevel);

                        if (!installObjectResult.Success)
                            throw new InvalidObjectException(installObjectResult.LogMessage, installObjectResult.Exception);
                    }
                }
            }

            // Add to InstalledPackages in database
            var success = await packageTrackingRepository.AddPackgageVersion(unpackedPackage.Name, unpackedPackage.Version);
            if (success == 1)
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