using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Simplic.Package.Data.DB;
using Unity;

namespace Simplic.Package.Service.Install
{
    public class CheckDependencyService : ICheckDependencyService
    {
        private readonly IPackageTrackingRepository versionService;
        public CheckDependencyService(IPackageTrackingRepository versionService)
        {
            this.versionService = versionService;
        }

        public async Task<CheckDependencyResult> Check(Dependency dependency)
        {
            var result = new CheckDependencyResult
            {
                Dependency = dependency
            };

            var versions = await versionService.GetPackageVersions(dependency.PackageName);
            var versionsList = versions.ToList();
            versionsList.Sort((x, y) => x.CompareTo(y));

            if (versions.Contains(dependency.Version) || (dependency.GreaterAllowed && versions.Where(x => x >= dependency.Version).Any()))
            {
                result.Exists = true;
                result.LatestExistingVersion = versionsList.Last();
            } else if (versions.Any())
            {
                result.Exists = false;
                result.LatestExistingVersion = versionsList.Last();
            } else
            {
                result.Exists = false;
                result.LatestExistingVersion = null;
            }

            return result;
        }
    }
}
