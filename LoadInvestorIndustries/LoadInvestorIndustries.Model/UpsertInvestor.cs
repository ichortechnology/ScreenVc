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
    public class UpsertInvestor
    {
        List<User> Investors { get; set; }
        UpsertInvestorTvp UpsertInvestorStoredProcedure { get; set; }
        IConfigurationProvider configurationProvider;

        public UpsertInvestor(List<User> investors, IConfigurationProvider configurationProvider)
        {
            this.Investors = investors;
            this.configurationProvider = configurationProvider;
        }

        DataTable CreateDataTable(List<User> investors)
        {
            // Create a DataTable without the primary key column, to match the table type of the stored procedure.
            DataTable investorTvpDataTable = new DataTable();

            investorTvpDataTable.Columns.Add(ColumnNames.ExternalInvestorId, typeof(Int32));
            investorTvpDataTable.Columns.Add(ColumnNames.Name, typeof(string));
            investorTvpDataTable.Columns.Add(ColumnNames.OnlineBioUrl, typeof(string));
            investorTvpDataTable.Columns.Add(ColumnNames.LinkedInUrl, typeof(string));
            investorTvpDataTable.Columns.Add(ColumnNames.ExternalInvestorSourceId, typeof(Int32));
            investorTvpDataTable.Columns.Add(ColumnNames.Updated, typeof(DateTime));

            foreach (User user in investors)
            {
                investorTvpDataTable.Rows.Add(user.Id, user.Name, user.OnlineBioUrl, user.LinkedinUrl, DomainData.AngelListExternalInvestorSourceId);
            }

            return investorTvpDataTable;
        }

        public void Execute()
        {
            var query = new UpsertInvestorTvp(CreateDataTable(this.Investors), configurationProvider);
            query.Execute();
        }
    }
}
