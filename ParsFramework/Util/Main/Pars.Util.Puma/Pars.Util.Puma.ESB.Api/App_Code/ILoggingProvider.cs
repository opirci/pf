using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.ESB.Api
{
    public interface ILoggingProvider
    {
        void WriteInfo(LogEntry entry);
        void WriteError(LogEntry entry);
        void WriteTrace(LogEntry entry);
        void WriteFatal(LogEntry entry);
        void WriteWarning(LogEntry entry);
    }
}
