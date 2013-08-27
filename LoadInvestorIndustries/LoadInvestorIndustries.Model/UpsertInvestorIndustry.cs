using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;

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

            // Use an instance of the generated dataset table to define the columns of the new table.
            ScreenVcDataSet.InvestorIndustryDataTable baseTable = new ScreenVcDataSet.InvestorIndustryDataTable();

            TvpDataTable.Columns.Add(baseTable.InvestorIdColumn.ColumnName, baseTable.InvestorIdColumn.DataType);
            TvpDataTable.Columns.Add(baseTable.IndustryIdColumn.ColumnName, baseTable.IndustryIdColumn.DataType);

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
