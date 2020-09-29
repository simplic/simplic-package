using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class InstallService : IInstallService
    {
        private readonly IUnityContainer container;
        public InstallService(IUnityContainer container)
        {
            this.container = container;
        }

        public async Task Install(Package unpackedPackage)
        {
            var checkDependencyService = container.Resolve<ICheckDependencyService>();
            
            var checkDependencyResults = new List<CheckDependencyResult>();
            foreach (var dependency in unpackedPackage.Dependencies)
                checkDependencyResults.Add(await checkDependencyService.Check(dependency));

            var missingDependencyResults = checkDependencyResults.Where(x => !x.Exists); // Give these as output
            
            var first = missingDependencyResults.FirstOrDefault();
            if (first != null)
                throw new MissingDependencyException($"{first.Dependency.PackageName} with Version {first.Dependency.Version} doesnt exist. Latest found Version: {first.LatestExistingVersion}");


            foreach (var item in unpackedPackage.UnpackedObjects)
            {
                var installService = container.Resolve<IInstallObjectService>(item.Key); // TODO: Exception handeling

                foreach (var installableObject in item.Value)
                {
                    var install = installableObject.Mode == MigrationMode.Deploy;
                    if (installableObject.Mode == MigrationMode.Migrate)
                    {
                        var result = await installService.CheckMigration(installableObject);
                        install = result.CanMigrate;
                    }

                    if (install)
                        await installService.InstallObject(installableObject);
                }
            }
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