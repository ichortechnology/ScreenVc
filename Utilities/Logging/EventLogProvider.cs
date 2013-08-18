using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Screen.Vc.Interfaces;
using Screen.Vc.Interfaces.Exceptions;

namespace Screen.Vc.Utilities.Logging
{
    public class EventLogProvider : LogProvider
    {
        public EventLogProvider(IIocContainer container, string providerName, string eventSource = defaultEventSource) : base(container)
        {
            base.ProviderName = providerName;
            this.EventSource = eventSource;

            if (!EventLog.SourceExists(eventSource))
            {
                EventLog.CreateEventSource(eventSource, "Application");
            }
        }

        public override void LogMessage(LogLevel logLevel, Guid coRelationId, string message, Object[] arguments)
        {
            StringBuilder           logMessage = new StringBuilder();
            EventLogEntryType       logEntryType = MapLogLevelToEventLogEntryType(logLevel);

            logMessage.AppendFormat("Correlation Id = {0}", coRelationId);
            logMessage.AppendFormat(message, arguments);
            EventLog.WriteEntry(EventSource, logMessage.ToString(), logEntryType);
        }

        public override void LogCritical(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Critical, coRelationId, message, arguments);
        }

        public override void LogError(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Error, coRelationId, message, arguments);
        }

        public override void LogWarning(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Warning, coRelationId, message, arguments);
        }

        public override void LogInformation(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Information, coRelationId, message, arguments);
        }

        public override void LogVerbose(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Verbose, coRelationId, message, arguments);
        }

        #region Private Methods

        private EventLogEntryType MapLogLevelToEventLogEntryType(LogLevel logLevel)
        {
            EventLogEntryType       eventLogEntryType;


            switch(logLevel)
            {
                case LogLevel.Critical:
                    eventLogEntryType = EventLogEntryType.Error;
                    break;

                case LogLevel.Error:
                case LogLevel.Warning:
                    eventLogEntryType = EventLogEntryType.Warning;
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                    eventLogEntryType = EventLogEntryType.Information;
                    break;

                default:
                    throw new ScreenVcException("Unknown LogLevel found.");
            }
            return eventLogEntryType;
        }

        #endregion

        public string EventSource { get; set; }

        private const string             defaultEventSource = "Application";
    }
}
