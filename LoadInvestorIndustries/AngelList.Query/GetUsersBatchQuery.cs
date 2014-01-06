using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.JsonTypes;

namespace AngelList.Query
{
    /// <summary>
    /// Given a list of user ids, returns User objects to a callback.
    /// </summary>
    public class GetUsersBatchQuery : AngelListPagedQuery<List<User>>
    {
        public List<int> Ids { get; protected set; }

        public GetUsersBatchQuery(List<int> ids, AsyncCallback batchCallback,  IAngelListClient angelListClient, LogWriter logWriter)
            : base(batchCallback, angelListClient, logWriter)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            this.Ids = ids;
        }

        protected override void Execute()
        {
            if (Ids == null)
            {
                throw new InvalidOperationException("Ids cannot be null.");
            }

            // Query in a loop to respect AngelListClient.QueryParameterLimit.

            for (int queryStartIndex = 0; queryStartIndex <= Ids.Count; queryStartIndex += AngelListClient.QueryParameterLimit)
            {
                int queryCount = (Ids.Count - queryStartIndex > AngelListClient.QueryParameterLimit) ? AngelListClient.QueryParameterLimit : Ids.Count - queryStartIndex;

                var ids = Ids.GetRange(queryStartIndex, queryCount);

                try
                {
                    List<User> batch = AngelListClient.UsersBatch(ids);

                    if (batch == null)
                    {
                        batch = new List<User>();
                    }
                    CallBatchCallback(batch);
                }
                catch (AngelListClientException ex)
                {
                    var entry = new LogEntry();
                    entry.Categories = new string[] { "General", "Warning" };
                    entry.Message = string.Format("An exception occurred when calling the service. No further ids will be processed. Id list: {0}. Exception: {1}", String.Join(",", ids), ex);
                    entry.Severity = TraceEventType.Error;
                    defaultLogWriter.Write(entry);

                    break;
                }
            }

        }
    }
}
