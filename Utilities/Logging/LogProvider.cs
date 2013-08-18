using Screen.Vc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Utilities.Logging
{
    public abstract class LogProvider : ILogProvider
    {
        public LogProvider(IIocContainer container)
        {
            Container = container;
        }

        public abstract void LogMessage(LogLevel logLevel, Guid coRelationId, string message, Object[] arguments);
        public abstract void LogCritical(Guid coRelationId, string message, Object[] arguments);
        public abstract void LogError(Guid coRelationId, string message, Object[] arguments);
        public abstract void LogWarning(Guid coRelationId, string message, Object[] arguments);
        public abstract void LogInformation(Guid coRelationId, string message, Object[] arguments);
        public abstract void LogVerbose(Guid coRelationId, string message, Object[] arguments);
        public string ProviderName { get; protected set; }

        protected IIocContainer Container { get; private set; }
    }
}
