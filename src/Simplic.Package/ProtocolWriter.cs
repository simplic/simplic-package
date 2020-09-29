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

        public event EventHandler<AddMessageEventArgs> InfoMessageAdded;
        public event EventHandler<AddMessageEventArgs> WarningMessageAdded;
        public event EventHandler<AddMessageEventArgs> ErrorMessageAdded;
        public event EventHandler<AddMessageEventArgs> DebugMessageAdded;

        public ProtocolWriter(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void WriteInfo(string message)
        {
            protocol.Info.Add(message);
            InfoMessageAdded?.Invoke(this, new AddMessageEventArgs { Message = message });
        }

        public void WriteWarning(string message)
        {
            protocol.Warning.Add(message);
            WarningMessageAdded?.Invoke(this, new AddMessageEventArgs { Message = message });
        }

        public void WriteError(string message)
        {
            protocol.Error.Add(message);
            ErrorMessageAdded?.Invoke(this, new AddMessageEventArgs { Message = message });
        }

        public void WriteDebug(string message)
        {
            protocol.Debug.Add(message);
            DebugMessageAdded?.Invoke(this, new AddMessageEventArgs { Message = message });
        }
        public Protocol GetProtocol => protocol;
    }
}
