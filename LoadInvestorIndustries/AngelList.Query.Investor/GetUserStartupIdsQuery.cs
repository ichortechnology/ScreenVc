using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AngelList.Interfaces;
using AngelList.Query;
using AngelList.JsonTypes;

namespace AngelList.Query.Investor
{
    /// <summary>
    /// Given a user id, returns the ids of the startups of the user.
    /// </summary>
    public class GetUserStartupIdsQuery : AngelListQuery<List<int>>
    {
        public override List<int> Result { get; protected set; }

        public int Id { get; protected set; }

        public GetUserStartupIdsQuery(int id, IAngelListClient angelListClient)
            : base(angelListClient)
        {
            this.Id = id;
        }

        public override List<int> Execute()
        {
            List<int> startupIds = new List<int>();

            int page = 0;
            int lastPage = int.MaxValue;

            do
            {
                UserRoles response = AngelListClient.UserRoles(Id, page);

                if (response.StartupRoles == null)
                {
                    response.StartupRoles = new StartupRole[0];
                }

                foreach (var startup in response.StartupRoles)
                {
                    startupIds.Add(startup.Startup.Id);
                } 

                page = response.Page;
                lastPage = response.LastPage;
            }
            while (page++ < lastPage);

            return startupIds;
        }
    }
}
