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
    public class UpsertIndustry
    {
        List<Market> Markets { get; set; }
        UpsertInvestorTvp UpsertIndustryStoredProcedure { get; set; }
        IConfigurationProvider configurationProvider;

        public UpsertIndustry(List<Market> markets, IConfigurationProvider configurationProvider
)
        {
            this.Markets = markets;
            this.configurationProvider = configurationProvider;
        }

        DataTable CreateDataTable(List<Market> markets)
        {
            // Create a DataTable without the primary key column, to match the table type of the stored procedure.
            DataTable TvpDataTable = new DataTable();

            TvpDataTable.Columns.Add(ColumnNames.ExternalIndustryId, typeof(Int32));
            TvpDataTable.Columns.Add(ColumnNames.Name, typeof(string));
            TvpDataTable.Columns.Add(ColumnNames.DisplayName, typeof(string));
            TvpDataTable.Columns.Add(ColumnNames.Updated, typeof(DateTime));

            foreach (Market market in markets)
            {
                TvpDataTable.Rows.Add(market.Id, market.Name, market.DisplayName);
            }

            return TvpDataTable;
        }

        public void Execute()
        {
            var query = new UpsertIndustryTvp(CreateDataTable(this.Markets), configurationProvider);
            query.Execute();
        }
    }
}
