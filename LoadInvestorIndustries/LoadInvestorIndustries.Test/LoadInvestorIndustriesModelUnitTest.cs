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
            List<User> investors = new List<User>();

            //
            // Test insert.
            //

            string nonce = DateTime.Now.Ticks.ToString();
            investors.Add(new User() { Id = 1, Name = "Investor 1" + nonce, OnlineBioUrl = "http://bio1" + nonce, LinkedinUrl = "linkedinurl 1" + nonce });
            investors.Add(new User() { Id = 2, Name = "Investor 2" + nonce, OnlineBioUrl = "http://bio2" + nonce, LinkedinUrl = "linkedinurl 2" + nonce });

            sqlCommonTasks.DeleteTableData();

            var upsertInvestor = new UpsertInvestor(investors, configurationProvider);
            upsertInvestor.Execute();


            // Check results.
            var investorsTable = new ScreenVcDataSet.InvestorDataTable();
            using (InvestorTableAdapter adapter = new InvestorTableAdapter())
            {
                adapter.Fill(investorsTable);

                foreach (var investor in investors)
                {
                    var rows = from row in investorsTable
                               where investor.Id == row.ExternalId
                                    && investor.Name == row.Name
                                    && investor.OnlineBioUrl == row.OnlineBioUrl
                                    && investor.LinkedinUrl == row.LinkedInUrl
                               select row;

                    Assert.AreEqual(1, rows.ToList().Count);

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


            // Check results.
            investorsTable = new ScreenVcDataSet.InvestorDataTable();
            using (InvestorTableAdapter adapter = new InvestorTableAdapter())
            {
                adapter.Fill(investorsTable);

                foreach (var investor in investors)
                {
                    var rows = from row in investorsTable
                               where investor.Id == row.ExternalId
                                    && investor.Name == row.Name
                                    && investor.OnlineBioUrl == row.OnlineBioUrl
                                    && investor.LinkedinUrl == row.LinkedInUrl
                               select row;

                    Assert.AreEqual(1, rows.ToList().Count);

                }
            }

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
