using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using AngelList.Interfaces;
using AngelList.JsonTypes;
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

        UserRoles IAngelListClient.UserRoles(int userId)
        {
            return ((IAngelListClient)this).UserRoles(userId, 1);
        }

        UserRoles IAngelListClient.UserRoles(int userId, int page)
        {
            Uri uri = new Uri(string.Format("{0}/users/{1}/roles?page={2}", BaseAddress, userId, page));

            UserRoles response = new UserRoles();
            try
            {
                response = JsonConvert.DeserializeObject<UserRoles>(RestClient.Get(uri));
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
