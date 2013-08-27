using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Query;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;

namespace AngelList.Query.Investor
{
    /// <summary>
    /// Given a list of user ids, returns the user objects of only the users who are investors.
    /// </summary>
    public class GetInvestorsQuery : AngelListPagedQuery<List<User>>
    {
        List<int> Ids { get; set; }

        public GetInvestorsQuery(List<int> ids, AsyncCallback batchCallback, IAngelListClient angelListClient, LogWriter logWriter)
            : base(batchCallback, angelListClient, logWriter)
        {
            this.Ids = ids;
        }

        protected override void Execute()
        {
            var usersBatch = new GetUsersBatchQuery(Ids, new AsyncCallback(this.ProcessUsersBatchResult), AngelListClient, defaultLogWriter);
            AsyncResult result = (AsyncResult)usersBatch.BeginExecute();
            result.AsyncWaitHandle.WaitOne();
        }

        public void ProcessUsersBatchResult(IAsyncResult iaResult)
        {
            BatchAsyncResult<List<User>> aresult = (BatchAsyncResult<List<User>>)iaResult;
            List<User> users = aresult.Result;

            if (users != null)
            {
                var investors = users.Where<User>(u => u.Investor == true);
                CallBatchCallback(new List<User>(investors.ToList()));
            }
            // Signal that processing of the IAsyncResult is completed.
            aresult.SignalCompleted();
        }
    }
}
