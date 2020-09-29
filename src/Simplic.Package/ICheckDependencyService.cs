using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface ICheckDependencyService
    {
        /// <summary>
        /// Checks if a given dependecy is satisfied or not
        /// </summary>
        /// <param name="dependency">The dependency to check</param>
        /// <returns>A CheckDependecyResult object</returns>
        Task<CheckDependencyResult> Check(Dependency dependency);
    }
}
