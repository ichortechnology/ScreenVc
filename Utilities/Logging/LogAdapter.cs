using Screen.Vc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Utilities.Logging
{
    public class LogAdapter : ILogProvider
    {
        public LogAdapter(IIocContainer container)
        {
            this.Container = container;
        }

        public void AddLogProvider(ILogProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            logProviders.Add(provider);
        }

        #region ILogProvider implementation

        public void LogMessage(LogLevel logLevel, Guid coRelationId, string message, object[] arguments)
        {
            foreach (ILogProvider provider in logProviders)
            {
                provider.LogMessage(logLevel, coRelationId, message, arguments);
            }
        }

        public void LogCritical(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Critical, coRelationId, message, arguments);
        }

        public void LogError(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Error, coRelationId, message, arguments);
        }

        public void LogWarning(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Warning, coRelationId, message, arguments);
        }

        public void LogInformation(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Information, coRelationId, message, arguments);
        }

        public void LogVerbose(Guid coRelationId, string message, Object[] arguments)
        {
            LogMessage(LogLevel.Verbose, coRelationId, message, arguments);
        }

        #endregion ILogProvider implementation

        public IIocContainer                Container { get; protected set; }


        private List<ILogProvider>           logProviders = new List<ILogProvider>();
    }
}
