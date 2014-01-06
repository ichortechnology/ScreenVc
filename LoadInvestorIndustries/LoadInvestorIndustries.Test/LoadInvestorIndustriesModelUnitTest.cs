using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LoadInvestorIndustries.Model;
using Screen.Vc.DataAccess.Investors;
using AngelList.JsonTypes;

namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class LoadInvestorIndustriesModelUnitTest
    {
        ConfigurationProvider configurationProvider;
        public static SqlCommonTasks sqlCommonTasks;

        [TestInitialize]
        public void InitializeTest()
        {
            ConfigurationProvider config = new ConfigurationProvider();
            config.Add(ConfigName.SqlConnectionString, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.ScreenVcConnectionString);
            config.Add(ConfigName.SqlConnectTimeoutInSeconds, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.SqlConnectTimeoutInSeconds);
            this.configurationProvider = config;
            sqlCommonTasks = new SqlCommonTasks(this.configurationProvider);
        }

        [TestMethod]
        public void UpsertInvestor_Success()
        {

            string nonce = DateTime.Now.Ticks.ToString();
            sqlCommonTasks.DeleteTableData();

            //
            // Test insert.
            //
            List<User> investors = new List<User>();
            investors.Add(new User() { Id = 1, Name = "Investor 1" + nonce, OnlineBioUrl = "http://bio1" + nonce, LinkedinUrl = "linkedinurl 1" + nonce });
            investors.Add(new User() { Id = 2, Name = "Investor 2" + nonce, OnlineBioUrl = "http://bio2" + nonce, LinkedinUrl = "linkedinurl 2" + nonce });
            var upsertInvestor = new UpsertInvestor(investors, configurationProvider);
            upsertInvestor.Execute();


            // Check results.
            using (SqlConnection dbConnection = new SqlConnection(configurationProvider.GetConfiguration(ConfigName.SqlConnectionString)))
            {
                dbConnection.Open();

                using (SqlCommand countTable = dbConnection.CreateCommand())
                {
                    countTable.CommandText = "SELECT ExternalId, Name, OnlineBioUrl, LinkedInUrl FROM dbo.ExternalInvestor";
                    countTable.CommandType = CommandType.Text;
                    var reader = countTable.ExecuteReader();

                    // Go through 1st result set. 
                    var externalId = reader["ExternalId"];
                }
            }

            //
            // Test update.
            //
            investors.Clear();
            nonce = DateTime.Now.Ticks.ToString();
            investors.Add(new User() { Id = 1, Name = "Investor 1" + nonce, OnlineBioUrl = "http://bio1" + nonce, LinkedinUrl = "linkedinurl 1" + nonce });
            investors.Add(new User() { Id = 2, Name = "Investor 2" + nonce, OnlineBioUrl = "http://bio2" + nonce, LinkedinUrl = "linkedinurl 2" + nonce });

            upsertInvestor = new UpsertInvestor(investors, configurationProvider);
            upsertInvestor.Execute();

        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void UpsertInvestor_InvalidInputData_Exception()
        {
            using (var dbConnection = new SqlConnection(Screen.Vc.DataAccess.Investors.Properties.Settings.Default.ScreenVcConnectionString))
            {
                List<User> investors = new List<User>();
                // Make a User with no Name. Name is NOT NULL in the database.
                investors.Add(new User() { Id = 1 });

                var upsertInvestor = new UpsertInvestor(investors, configurationProvider);
                upsertInvestor.Execute();
            }
        }
    }
}
