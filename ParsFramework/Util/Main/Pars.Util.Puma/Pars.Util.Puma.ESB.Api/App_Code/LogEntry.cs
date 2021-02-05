using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.ESB.Api
{
    public class LogEntry
    {
        public string AccountName { get; set; }

        public string CommandName { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public string EntryCode { get; set; }

        public int ProcessSecond { get; set; }

        public Dictionary<string, string> Dictionaries { get; set; }
    }
}