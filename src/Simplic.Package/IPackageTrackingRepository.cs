using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IPackageTrackingRepository
    {
        Task<IEnumerable<Version>> GetPackageVersions(string packageName);
        Task<Version> GetLatestPackageVersion(string packageName);
        Task<int> AddPackgageVersion(string packageName, Version version);
    }
}
