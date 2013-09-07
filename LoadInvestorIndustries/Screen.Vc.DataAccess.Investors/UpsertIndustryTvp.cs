using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.DataAccess.Investors
{
    public class UpsertIndustryTvp : NonQueryAzureStoredProcedure
    {
        DataTable IndustryDataTable { get; set; }

        public UpsertIndustryTvp(DataTable industryDataTable, IConfigurationProvider configurationProvider)
            : base(configurationProvider)
        {
            this.IndustryDataTable = industryDataTable;
        }

        protected override string GetStoredProcedureName()
        {
            return StoredProcedureName.UpsertIndustryTvp.ToString();
        }

        protected override SqlParameter[] GetParameters()
        {
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@industryTvp", SqlDbType.Structured);
            parameters[0].Value = IndustryDataTable;

            return parameters;
        }

        protected override void CheckNonQueryReturnValue(int rowsAffected)
        {
            // Every row in the DataTable should be either updated or inserted.
            if (rowsAffected != IndustryDataTable.Rows.Count)
            {
                // TODO, 6, Screen.Vc.DataAccess.Investors: Log warning when non-query return value is not the expected value.
            }
        }
    }
}
