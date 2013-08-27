using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;
using RestClient;

namespace AngelList.JsonNet
{
    /// <summary>
    /// Implementation of IAngelListClient using the Json.NET serialization library. Requires an implementation of IRestClient.
    /// </summary>
    public class JsonNetAngelListClient : IAngelListClient
    {
        IRestClient RestClient { get; set; }
        public string BaseAddress { get; private set; }

        public JsonNetAngelListClient(string baseAddress, IRestClient restClient)
        {
            if (restClient == null)
            {
                throw new ArgumentNullException("restClient");
            }

            this.RestClient = restClient;
            this.BaseAddress = baseAddress;
        }

        int IAngelListClient.QueryParameterLimit
        {
            get { return 50; }
        }

        int IAngelListClient.QueryObjectResultsLimit
        {
            get { return 100; }
        }

        int IAngelListClient.QueryIdResultsLimit
        {
            get { return 1000; }
        }

        int IAngelListClient.QueryActivityFeedResultsLimit
        {
            get { return 25; }
        }

        User IAngelListClient.Users(int id)
        {
            throw new NotImplementedException();
        }

        UserDetails IAngelListClient.UserDetails(int id, UsersIncludeDetails detailsToInclude)
        {
            throw new NotImplementedException();
        }

        List<User> IAngelListClient.UsersBatch(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            if (ids.ToList().Count == 0)
            {
                return new List<User>();
            }

            string idsCommaSeparated = String.Join(",", ids);

            Uri uri = new Uri(string.Format("{0}/users/batch?ids={1}", BaseAddress, idsCommaSeparated));

            List<User> users;
            try
            {
                users = JsonConvert.DeserializeObject<List<User>>(RestClient.Get(uri));
            }
            catch (RestClientException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            catch (JsonException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            return users;
        }

        UsersRolesResponse IAngelListClient.UsersRoles(int userId)
        {
            Uri uri = new Uri(string.Format("{0}/users/{1}/roles", BaseAddress, userId));

            UsersRolesResponse response = new UsersRolesResponse();
            try
            {
                response = JsonConvert.DeserializeObject<UsersRolesResponse>(RestClient.Get(uri));
            }
            catch (RestClientException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            catch (JsonException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            return response;
        }

        UsersRolesResponse IAngelListClient.UsersRoles(int userId, int page)
        {
            Uri uri = new Uri(string.Format("{0}/users/{1}/roles?page={2}", BaseAddress, userId, page));

            UsersRolesResponse response = new UsersRolesResponse();
            try
            {
                response = JsonConvert.DeserializeObject<UsersRolesResponse>(RestClient.Get(uri));
            }
            catch (RestClientException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            catch (JsonException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            return response;
        }

        JsonTypes.Startup IAngelListClient.Startups(int id)
        {
            throw new NotImplementedException();
        }

        List<JsonTypes.Startup> IAngelListClient.StartupsBatch(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            if (ids.ToList().Count == 0)
            {
                return new List<JsonTypes.Startup>();
            }

            string idsCommaSeparated = String.Join(",", ids);

            Uri uri = new Uri(string.Format("{0}/startups/batch?ids={1}", BaseAddress, idsCommaSeparated));

            List<JsonTypes.Startup> startups;
            try
            {
                startups = JsonConvert.DeserializeObject<List<JsonTypes.Startup>>(RestClient.Get(uri));
            }
            catch (RestClientException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            catch (JsonException ex)
            {
                throw new AngelListClientException("An exception occurred when calling the service. See inner exception.", ex);
            }
            return startups;
        }
    }
}
