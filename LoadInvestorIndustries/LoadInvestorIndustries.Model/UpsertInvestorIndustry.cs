using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using AngelList.JsonTypes;
using Screen.Vc.DataAccess.Investors;

namespace LoadInvestorIndustries.Model
{
    public class UpsertInvestorIndustry
    {
        List<UserIdMarketId> InvestorIdIndustryId { get; set; }
        UpsertInvestorIndustryTvp UpsertInvestorIndustryStoredProcedure { get; set; }
        IConfigurationProvider configurationProvider;

        public UpsertInvestorIndustry(List<UserIdMarketId> investorIdIndustryId, IConfigurationProvider configurationProvider)
        {
            this.InvestorIdIndustryId = investorIdIndustryId;
            this.configurationProvider = configurationProvider;
        }

        DataTable CreateDataTable(List<UserIdMarketId> investorIdIndustryId)
        {
            // Create a DataTable without the primary key column, to match the table type of the stored procedure.
            DataTable TvpDataTable = new DataTable();

            TvpDataTable.Columns.Add(ColumnNames.ExternalInvestorId, typeof(Int32));
            TvpDataTable.Columns.Add(ColumnNames.ExternalIndustryId, typeof(Int32));

            foreach (UserIdMarketId pair in investorIdIndustryId)
            {
                TvpDataTable.Rows.Add(pair.UserId, pair.MarketId);
            }

            return TvpDataTable;
        }

        public void Execute()
        {
            var query = new UpsertInvestorIndustryTvp(CreateDataTable(this.InvestorIdIndustryId), configurationProvider);
            query.Execute();
        }
    }
}
