using System.Collections.Generic;

namespace Simplic.Package
{
    public class CheckDependenciesResult : LogResult
    {
        public IList<Dependency> MissingDependencies { get; set; }
    }
}