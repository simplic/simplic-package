using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    // TODO: There has to be a better name ...
    public interface IInitializePackageSystemService
    {
        Task<InitializePackageSystemResult> Initialize();
    }
}
