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

        public GetStartupsMarketsQuery(List<int> startupIds, IAngelListClient angelListClient, LogWriter logWriter)
            : this(startupIds, null, angelListClient, logWriter)
        {
        }

        public GetStartupsMarketsQuery(List<int> startupIds, object state, IAngelListClient angelListClient, LogWriter logWriter)
            : base(state, angelListClient, logWriter)
        {
            this.Ids = startupIds;
        }

        public override Object Execute()
        {
            var startupsBatchQuery = new GetStartupsBatchQuery(Ids, AngelListClient, defaultLogWriter);
            List<Startup> results = (List<Startup>)startupsBatchQuery.Execute();
            return ProcessStartupsBatchQuery(results);
        }

        public Dictionary<int, List<Market>> ProcessStartupsBatchQuery(List<Startup> startups)
        {
            var startupIdsMarkets = new Dictionary<int, List<Market>>();

            foreach (var startup in startups)
            {
                startupIdsMarkets.Add(startup.Id, startup.Markets.ToList());
            }

            return startupIdsMarkets;
        }
    }
}
