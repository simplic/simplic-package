using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class ProtocolWriter
    {
        private Protocol protocol;

        public event EventHandler<LogMessageEventArgs> InfoMessageAdded;
        public event EventHandler<LogMessageEventArgs> WarningMessageAdded;
        public event EventHandler<LogMessageEventArgs> ErrorMessageAdded;
        public event EventHandler<LogMessageEventArgs> DebugMessageAdded;

        public ProtocolWriter(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void WriteInfo(string message)
        {
            protocol.Info.Add(message);
            InfoMessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message });
        }

        public void WriteWarning(string message)
        {
            protocol.Warning.Add(message);
            WarningMessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message });
        }

        public void WriteError(string message)
        {
            protocol.Error.Add(message);
            ErrorMessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message });
        }

        public void WriteDebug(string message)
        {
            protocol.Debug.Add(message);
            DebugMessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message });
        }
        public Protocol GetProtocol => protocol;
    }
}
