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

            // Use an instance of the generated dataset table to define the columns of the new table.
            ScreenVcDataSet.InvestorDataTable baseTable = new ScreenVcDataSet.InvestorDataTable();

            investorTvpDataTable.Columns.Add(baseTable.ExternalIdColumn.ColumnName, baseTable.ExternalIdColumn.DataType);
            investorTvpDataTable.Columns.Add(baseTable.NameColumn.ColumnName, baseTable.NameColumn.DataType);
            investorTvpDataTable.Columns.Add(baseTable.OnlineBioUrlColumn.ColumnName, baseTable.OnlineBioUrlColumn.DataType);
            investorTvpDataTable.Columns.Add(baseTable.LinkedInUrlColumn.ColumnName, baseTable.LinkedInUrlColumn.DataType);

            foreach (User user in investors)
            {
                investorTvpDataTable.Rows.Add(user.Id, user.Name, user.OnlineBioUrl, user.LinkedinUrl);
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
