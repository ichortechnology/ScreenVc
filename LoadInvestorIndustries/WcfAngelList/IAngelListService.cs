using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;

namespace AngelList.Wcf
{
    [ServiceContract]
    internal interface IAngelListService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/users/{id}")]
        User Users(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/users/{id}?include_details={detailsToInclude}")]
        UserDetails UserDetails(string id, string detailsToInclude);

        [OperationContract]
        [WebGet(UriTemplate = "/users/batch?ids={idsCommaSeparated}")]
        List<User> UsersBatch(string idsCommaSeparated);

        [OperationContract]
        [WebGet(UriTemplate = "/users/{userId}/roles")]
        UsersRolesResponse UsersRoles(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/users/{userId}/roles?page={page}")]
        UsersRolesResponse UsersRolesPage(string userId, string page);

        [OperationContract]
        [WebGet(UriTemplate = "/startups/{id}")]
        AngelList.JsonTypes.Startup Startups(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/startups/batch?ids={idsCommaSeparated}")]
        List<AngelList.JsonTypes.Startup> StartupsBatch(string idsCommaSeparated);
    }
}
