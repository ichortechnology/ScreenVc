using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AngelList.Interfaces;
using AngelList.Query;
using AngelList.JsonTypes;

namespace AngelList.Query.Investor
{
    /// <summary>
    /// Given a list of user ids, returns the ids of the startups of the users to a callback.
    /// </summary>
    public class GetUsersStartupIdsQuery : AngelListQuery<Dictionary<int, List<int>>>
    {
        public override Dictionary<int, List<int>> Result { get; protected set; }

        public List<int> Ids { get; set; }

        public GetUsersStartupIdsQuery(List<int> ids, IAngelListClient angelListClient)
            : base(angelListClient)
        {
            this.Ids = ids;
        }

        public override Dictionary<int, List<int>> Execute()
        {
             Dictionary<int, List<int>> userIdStartupIdsDict = new Dictionary<int, List<int>>();

            // Get a list of startup ids for each user.
            foreach (int id in Ids)
            {
                var userStartupIdsQuery = new GetUserStartupIdsQuery(id, this.AngelListClient);
                List<int> userStartupIds = userStartupIdsQuery.Execute();
                userIdStartupIdsDict.Add(id, userStartupIds);
            }
            return userIdStartupIdsDict;
        }
    }
}
