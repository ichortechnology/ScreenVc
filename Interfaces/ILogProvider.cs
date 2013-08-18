using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Interfaces
{
    public enum LogLevel
    {
        /// <summary>
        /// Critical error means system is in dire condition. Typically requires 
        /// a human to look at it and resolve. E.g. Database down.
        /// </summary>
        Critical,

        /// <summary>
        /// Error typically are errors and are transient in nature.
        /// </summary>
        Error,

        /// <summary>
        /// </summary>
        Warning,

        /// <summary>
        /// Informational logs. Typically used to provide more information about an event 
        /// or step.
        /// </summary>
        Information,

        /// <summary>
        /// Verbose logs. Typically used to provide step logging. Very useful when debugging
        /// or tracing information.
        /// </summary>
        Verbose
    }

    public interface ILogProvider
    {
        void LogMessage(LogLevel logLevel, Guid coRelationId, string message, Object[] arguments);
        void LogCritical(Guid coRelationId, string message, Object[] arguments);
        void LogError(Guid coRelationId, string message, Object[] arguments);
        void LogWarning(Guid coRelationId, string message, Object[] arguments);
        void LogInformation(Guid coRelationId, string message, Object[] arguments);
        void LogVerbose(Guid coRelationId, string message, Object[] arguments);
    }
}
