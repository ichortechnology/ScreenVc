using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.Query;
using AngelList.JsonTypes;

namespace AngelList.Query.Investor
{
    public class GetStartupsMarketsQuery : AngelListPagedQuery<Dictionary<int, List<Market>>>
    {
        public List<int> Ids { get; set; }

        public GetStartupsMarketsQuery(List<int> startupIds, AsyncCallback batchCallback, IAngelListClient angelListClient, LogWriter logWriter)
            : this(startupIds, null, batchCallback, angelListClient, logWriter)
        {
        }

        public GetStartupsMarketsQuery(List<int> startupIds, object state, AsyncCallback batchCallback, IAngelListClient angelListClient, LogWriter logWriter)
            : base(state, batchCallback, angelListClient, logWriter)
        {
            this.Ids = startupIds;
        }

        protected override void Execute()
        {
            var startupsBatchCallback = new AsyncCallback(ProcessStartupsBatchQuery);

            var startupsBatchQuery = new GetStartupsBatchQuery(Ids, startupsBatchCallback, AngelListClient, defaultLogWriter);

            var result = startupsBatchQuery.BeginExecute();
            result.AsyncWaitHandle.WaitOne();
        }

        public void ProcessStartupsBatchQuery(IAsyncResult iaResult)
        {
            var aresult = (BatchAsyncResult<List<AngelList.JsonTypes.Startup>>)iaResult;
            List<AngelList.JsonTypes.Startup> startups = aresult.Result;

            var startupIdsMarkets = new Dictionary<int, List<Market>>();

            foreach (var startup in startups)
            {
                startupIdsMarkets.Add(startup.Id, startup.Markets.ToList());
            }

            CallBatchCallback(startupIdsMarkets);

            // Signal that processing of the IAsyncResult is completed.
            aresult.SignalCompleted();
        }
    }
}
