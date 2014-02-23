using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.JsonTypes;

namespace AngelList.Query
{
    /// <summary>
    /// Given a list of Startup ids, returns the Startup objects to a callback.
    /// </summary>
    public class GetStartupsBatchQuery : AngelListPagedQuery<List<AngelList.JsonTypes.Startup>>
    {
        public List<int> Ids { get; protected set; }

        public GetStartupsBatchQuery(List<int> ids, IAngelListClient angelListClient, LogWriter logWriter)
            : base( angelListClient, logWriter)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            this.Ids = ids;
        }

        public override Object Execute()
        {
            if (Ids == null)
            {
                throw new InvalidOperationException("Ids cannot be null.");
            }

            List<int> batchIds = new List<int>();
            List<Startup> startupList = new List<Startup>();
            List<AngelList.JsonTypes.Startup> batch;

            // Query in a loop to respect AngelListClient.QueryParameterLimit.
            for (int queryStartIndex = 0; queryStartIndex <= Ids.Count; queryStartIndex += AngelListClient.QueryParameterLimit)
            {
                int queryCount = (Ids.Count - queryStartIndex > AngelListClient.QueryParameterLimit) ? AngelListClient.QueryParameterLimit : Ids.Count - queryStartIndex;

                batch = AngelListClient.StartupsBatch(Ids.GetRange(queryStartIndex, queryCount));

                if (batch == null)
                {
                    batch = new List<AngelList.JsonTypes.Startup>();
                }

                startupList.AddRange(batch);
            }

            return startupList;
        }
    }

}
