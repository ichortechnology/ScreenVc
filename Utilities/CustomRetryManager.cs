using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Utilities
{
    public class CustomRetryManager : RetryManager
    {
        public CustomRetryManager(IEnumerable<RetryStrategy> retryStrategies, string defaultStrategyName, IDictionary<string, string> defaultRetryStrategyNameMap)
            : base(retryStrategies, defaultStrategyName, defaultRetryStrategyNameMap)
        {
        }

        #region Private Member variables

        #endregion Private Member variables
    }
}
