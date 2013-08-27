using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadInvestorIndustries.Model
{
    public struct UserIdMarketId
    {
        public int UserId;
        public int MarketId;

        public UserIdMarketId(int userId, int marketId)
        {
            this.UserId = userId;
            this.MarketId = marketId;
        }
    }
}
