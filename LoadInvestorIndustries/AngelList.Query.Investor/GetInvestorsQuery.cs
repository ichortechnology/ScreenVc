using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.Query;
using AngelList.JsonTypes;

namespace AngelList.Query.Investor
{
    /// <summary>
    /// Given a list of user ids, returns the user objects of only the users who are investors.
    /// </summary>
    public class GetInvestorsQuery : AngelListPagedQuery<List<User>>
    {
        List<int> Ids { get; set; }

        public GetInvestorsQuery(List<int> ids, IAngelListClient angelListClient, LogWriter logWriter)
            : base(angelListClient, logWriter)
        {
            this.Ids = ids;
        }

        public override Object Execute()
        {
            List<User> results = AngelListClient.UsersBatch(Ids);
            return ProcessUsersBatchResult(results);
        }

        public List<User> ProcessUsersBatchResult(List<User> users)
        {
            if (users != null)
            {
                var investors = users.Where<User>(u => u.Investor == true);
                return investors.ToList();
            }
            return new List<User>();
        }
    }
}
