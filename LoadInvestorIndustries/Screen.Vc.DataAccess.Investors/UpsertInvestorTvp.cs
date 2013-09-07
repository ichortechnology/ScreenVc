using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.DataAccess.Investors
{
    public class UpsertInvestorTvp : NonQueryAzureStoredProcedure
    {
        DataTable InvestorDataTable { get; set; }

        public UpsertInvestorTvp(DataTable investorDataTable, IConfigurationProvider configurationProvider)
            : base(configurationProvider)
        {
            this.InvestorDataTable = investorDataTable;
        }

        protected override string GetStoredProcedureName()
        {
            return StoredProcedureName.UpsertInvestorTvp.ToString();
        }

        protected override SqlParameter[] GetParameters()
        {
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@investorTvp", SqlDbType.Structured);
            parameters[0].Value = InvestorDataTable;

            return parameters;
        }

        protected override void CheckNonQueryReturnValue(int rowsAffected)
        {
            // Every row in the DataTable should be either updated or inserted.
            if (rowsAffected != InvestorDataTable.Rows.Count)
            {
                // TODO, 6, Screen.Vc.DataAccess.Investors: Log warning when non-query return value is not the expected value.
            }
        }
    }
}
