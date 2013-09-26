using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;

using AngelList.Interfaces;
using Newtonsoft.Json;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;

namespace AngelList.Wcf
{
    /// <summary>
    /// Pure WCF mplementation of IAngelListClient.
    /// </summary>
    public class WcfAngelListClient : IAngelListClient 
    {
        public int QueryParameterLimit
        {
            get { return 50; }
        }

        public int QueryObjectResultsLimit
        {
            get { return 100; }
        }

        public int QueryIdResultsLimit
        {
            get { return 1000; }
        }

        public int QueryActivityFeedResultsLimit
        {
            get { return 25; }
        }

        public List<User> UsersBatch(IEnumerable<int> ids)
        {
            string idsCommaSeparated = String.Join(",", ids);

            using (WebChannelFactory<IAngelListService> cf = new WebChannelFactory<IAngelListService>("IAngelListService"))
            {
                IAngelListService channel = cf.CreateChannel();
                List<User> users = channel.UsersBatch(idsCommaSeparated);
                return users;
            }

        }

        public UsersRolesResponse UsersRoles(int userId)
        {
            using (WebChannelFactory<IAngelListService> cf = new WebChannelFactory<IAngelListService>("IAngelListService"))
            {
                IAngelListService channel = cf.CreateChannel();
                UsersRolesResponse response = channel.UsersRoles(Convert.ToString(userId));
                return response;
            }
        }

        public UsersRolesResponse UsersRoles(int userId, int page)
        {
            using (WebChannelFactory<IAngelListService> cf = new WebChannelFactory<IAngelListService>("IAngelListService"))
            {
                IAngelListService channel = cf.CreateChannel();
                UsersRolesResponse response = channel.UsersRolesPage(Convert.ToString(userId), page.ToString());
                return response;
            }
        }

        public List<JsonTypes.Startup> StartupsBatch(IEnumerable<int> ids)
        {
            using (WebChannelFactory<IAngelListService> cf = new WebChannelFactory<IAngelListService>("IAngelListService"))
            {
                IAngelListService channel = cf.CreateChannel();
                List<JsonTypes.Startup> response = channel.StartupsBatch(String.Join(",", ids));
                return response;
            }
        }
    }
}
