using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IPackService
    {
        Task<byte[]> Pack(string json);

        Task<byte[]> Pack(Package package);
    }
}
