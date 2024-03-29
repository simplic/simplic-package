﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="ICheckDependencyService"/>
    public class CheckDependencyService : ICheckDependencyService
    {
        private readonly IPackageTrackingRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="CheckDependencyService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public CheckDependencyService(IPackageTrackingRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<CheckDependenciesResult> CheckDependencies(IList<Dependency> dependencies)
        {
            var result = new CheckDependenciesResult
            {
                MissingDependencies = new List<Dependency>()
            };

            foreach (var dependency in dependencies)
            {
                var isSatisfied = await Check(dependency);

                if (!isSatisfied)
                    result.MissingDependencies.Add(dependency);
            }

            if (result.MissingDependencies.Any())
            {
                result.LogLevel = LogLevel.Error;

                result.Message = "Missing one or more dependencies. Missing: ";
                foreach (var dependency in result.MissingDependencies)
                    result.Message += $"{dependency.Package}, Version {dependency.Version}; ";
            }
            else
            {
                result.LogLevel = LogLevel.Info;
                result.Message = "All dependencies are satisfied.";
            }

            return result;
        }

        /// <summary>
        /// Checks if a given dependecy is satisfied or not
        /// </summary>
        /// <param name="dependency">The dependency to check</param>
        /// <returns>Whether the dependencies is satisfied</returns>
        public async Task<bool> Check(Dependency dependency)
        {
            var version = await repository.GetPackageVersion(dependency.Package);

            if (version == null)
                return false;

            if (version == dependency.Version || (version > dependency.Version && dependency.GreaterAllowed))
                return true;

            return false;
        }
    }
}