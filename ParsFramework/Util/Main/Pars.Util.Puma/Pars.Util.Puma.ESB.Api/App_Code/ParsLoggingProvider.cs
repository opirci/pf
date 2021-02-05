using Pars.Common.Logging.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.ESB.Api
{
    public class ParsLoggingProvider : ILoggingProvider
    {
        ILogger _logger;
        public ParsLoggingProvider(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(LogEntry entry)
        {
            _logger.SendAsync(ConvertLogCommand(entry, LogType.Error));
        }

        public void WriteFatal(LogEntry entry)
        {
            _logger.SendAsync(ConvertLogCommand(entry, LogType.Fatal));
        }

        public void WriteInfo(LogEntry entry)
        {
            _logger.SendAsync(ConvertLogCommand(entry, LogType.Info));
        }

        public void WriteTrace(LogEntry entry)
        {
            _logger.SendAsync(ConvertLogCommand(entry, LogType.Trace));
        }

        public void WriteWarning(LogEntry entry)
        {
            _logger.SendAsync(ConvertLogCommand(entry, LogType.Warn));
        }

        private LogCommand ConvertLogCommand(LogEntry entry, LogType logType)
        {
            return new LogCommand()
            {
                Message = entry.Message,
                Exception = entry.Exception,
                Name = entry.Name,
                AccountName = entry.AccountName,
                Type = logType,
                CommandName = entry.CommandName,
                Dictionaries = entry.Dictionaries,
                EntryCode = entry.EntryCode,
                EntryDate = DateTime.Now,
                ProcessIdentifier = System.Diagnostics.Trace.CorrelationManager.ActivityId.ToString(),
                ProcessSecond = entry.ProcessSecond,
                ApplicationId = ConfigurationManager.AppSettings["ApplicationGuid"]
            };
        }
    }
}