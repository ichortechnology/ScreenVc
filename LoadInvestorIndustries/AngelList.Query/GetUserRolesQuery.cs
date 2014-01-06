﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.JsonTypes;

namespace AngelList.Query
{
    /// <summary>
    /// Given a user id, returns the user's roles to a callback.
    /// </summary>
    public class GetUserRolesQuery : AngelListPagedQuery<UserStartupRoles>
    {
        public int Id { get; protected set; }

        public GetUserRolesQuery(int id, AsyncCallback batchCallback, IAngelListClient angelListClient, LogWriter logWriter)
            : base(batchCallback, angelListClient, logWriter)
        {
            this.Id = id;
        }

        protected override void Execute()
        {
            int page = 0;
            int lastPage = int.MaxValue;

            do
            {
                UserRoles response = AngelListClient.UserRoles(Id, page);

                if (response.StartupRoles == null)
                {
                    response.StartupRoles = new StartupRole[0];
                }
                
                CallBatchCallback(new UserStartupRoles(Id, response.StartupRoles.ToList()));

                page = response.Page;
                lastPage = response.LastPage;
            }
            while (page++ < lastPage);
        }
    }
}
