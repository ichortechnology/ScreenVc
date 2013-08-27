using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;

namespace AngelList
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

        User Users(int id);

        UserDetails UserDetails(int id, UsersIncludeDetails detailsToInclude);

        List<User> UsersBatch(IEnumerable<int> ids);

        UsersRolesResponse UsersRoles(int userId);

        UsersRolesResponse UsersRoles(int userId, int page);

        AngelList.JsonTypes.Startup Startups(int id);

        List<AngelList.JsonTypes.Startup> StartupsBatch(IEnumerable<int> ids);
    }
}
