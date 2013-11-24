using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.DataAccess.Investors
{
    public class UpsertInvestorIndustryTvp : NonQueryAzureStoredProcedure
    {
        DataTable InvestorIndustryDataTable { get; set; }

        public UpsertInvestorIndustryTvp(DataTable investorIndustryDataTable, IConfigurationProvider configurationProvider)
            : base(configurationProvider)
        {
            this.InvestorIndustryDataTable = investorIndustryDataTable;
        }

        protected override string GetStoredProcedureName()
        {
            return StoredProcedureName.sproc_ExternalInvestorExternalIndustry_Upsert.ToString();
        }

        protected override SqlParameter[] GetParameters()
        {
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@investorIndustryTvp", SqlDbType.Structured);
            parameters[0].Value = InvestorIndustryDataTable;

            return parameters;
        }

        protected override void CheckNonQueryReturnValue(int rowsAffected)
        {
            // Since we only insert, not update, don't know how many rows affected to expect.
        }
    }
}
