using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LoadInvestorIndustries.Model;
using Screen.Vc.DataAccess.Investors;
using Screen.Vc.DataAccess.Investors.ScreenVcDataSetTableAdapters;
using AngelList.JsonTypes;

namespace LoadInvestorIndustries.Test
{
    public class SqlCommonTasks
    {
        IConfigurationProvider configurationProvider;

        public SqlCommonTasks(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public void DeleteTableData()
        {
            using (SqlConnection dbConnection = new SqlConnection(configurationProvider.GetConfiguration(ConfigName.SqlConnectionString)))
            {
                dbConnection.Open();
               
                // delete table data.
                using (SqlCommand deleteTable = dbConnection.CreateCommand())
                {
                    deleteTable.CommandText = "delete from investorindustry" ;
                    deleteTable.CommandType = CommandType.Text;
                    deleteTable.ExecuteNonQuery();
                }

                using (SqlCommand deleteTable = dbConnection.CreateCommand())
                {
                    deleteTable.CommandText = "delete from investor";
                    deleteTable.CommandType = CommandType.Text;
                    deleteTable.ExecuteNonQuery();
                }

                using (SqlCommand deleteTable = dbConnection.CreateCommand())
                {
                    deleteTable.CommandText = "delete from industry";
                    deleteTable.CommandType = CommandType.Text;
                    deleteTable.ExecuteNonQuery();
                }
            }
        }

        public void GetTableCounts(out int investorCount, out int industryCount, out int investorIndustryCount)
        {

            using (SqlConnection dbConnection = new SqlConnection(configurationProvider.GetConfiguration(ConfigName.SqlConnectionString)))
            {
                dbConnection.Open();

                using (SqlCommand countTable = dbConnection.CreateCommand())
                {
                    countTable.CommandText = "select count(id) from investor";
                    countTable.CommandType = CommandType.Text;
                    object count = countTable.ExecuteScalar();

                    investorCount = Convert.ToInt32(count.ToString());
                }

                using (SqlCommand countTable = dbConnection.CreateCommand())
                {
                    countTable.CommandText = "select count(id) from industry";
                    countTable.CommandType = CommandType.Text;
                    object count = countTable.ExecuteScalar();

                    industryCount = Convert.ToInt32(count.ToString());
                }


                using (SqlCommand countTable = dbConnection.CreateCommand())
                {
                    countTable.CommandText = "select count(id) from investorindustry";
                    countTable.CommandType = CommandType.Text;
                    object count = countTable.ExecuteScalar();

                    investorIndustryCount = Convert.ToInt32(count.ToString());
                }

            }
        }


    }
}
