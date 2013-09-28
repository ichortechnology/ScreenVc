using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using AngelList.JsonTypes;

namespace AngelList.Interfaces
{
    /// <summary>
    /// Interface implemented by classes that call the AngelList REST API.
    /// </summary>
    /// <exception cref="AngelListClientException">Wraps known exceptions of implementations.</exception>
    public interface IAngelListClient
    {
        int QueryParameterLimit { get; }
        int QueryObjectResultsLimit { get; }
        int QueryIdResultsLimit { get; }
        int QueryActivityFeedResultsLimit { get; }

        List<User> UsersBatch(IEnumerable<int> ids);

        UserRoles UserRoles(int userId);

        UserRoles UserRoles(int userId, int page);

        List<AngelList.JsonTypes.Startup> StartupsBatch(IEnumerable<int> ids);
    }
}
