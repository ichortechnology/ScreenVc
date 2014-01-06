using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AngelList.JsonTypes;

namespace AngelList.Query
{
    /// <summary>
    /// Used by the GetUserRolesQuery class to return results.
    /// </summary>
    public class UserStartupRoles
    {
        int UserId { get; set; }
        List<StartupRole> Roles { get; set; }
        
        public UserStartupRoles()
        {
        }

        public UserStartupRoles(int userId, List<StartupRole> roles)
        {
            this.UserId = userId;
            this.Roles = roles;
        }
    }
}
