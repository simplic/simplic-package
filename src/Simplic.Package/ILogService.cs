using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface ILogService
    {
        event EventHandler<EventArgs> Logged;
        Task WriteAsync(string message, LogLevel logLevel);
    }
}
