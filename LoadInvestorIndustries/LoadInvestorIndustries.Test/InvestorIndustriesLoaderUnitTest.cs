using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

using AngelList;
using AngelList.Query;
using AngelList.Wcf;
using AngelList.JsonNet;
using AngelList.JsonTypes;
using RestClient;
using Screen.Vc.DataAccess.Investors;

namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class InvestorIndustriesLoaderUnitTest
    {
        public static IAngelListClient AngelListClient { get; set; }
        public static string BaseAddress { get; set; }
        public static QueryParameterParser paramParser;
        public static LogWriter defaultLogWriter;
        public static SqlCommonTasks sqlCommonTasks;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            BaseAddress = "http://api.angel.co/1";

            //AngelListClient = new WcfAngelListClient();

            IRestClient restClient = new SimpleRestClient();
            AngelListClient = new JsonNetAngelListClient(BaseAddress, restClient);

            defaultLogWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

            ConfigurationProvider config = new ConfigurationProvider();
            config.Add(ConfigName.SqlConnectionString, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.ScreenVcConnectionString);
            config.Add(ConfigName.SqlConnectTimeoutInSeconds, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.SqlConnectTimeoutInSeconds);
            sqlCommonTasks = new SqlCommonTasks(config);

        }

        [TestMethod]
        public void LoadInvestorsAndLoadUsersMarkets_155_160_Success()
        {
            int startId = 155;
            int endId = 160;
            paramParser = new QueryParameterParser(3, 2);

            sqlCommonTasks.DeleteTableData();

            RunLoadInvestorsAndLoadUsersMarkets(startId, endId);

            int investorCount;
            int industryCount;
            int investorIndustryCount;

            sqlCommonTasks.GetTableCounts(out investorCount, out industryCount, out investorIndustryCount);

            Assert.AreEqual(2, investorCount);
            Assert.AreEqual(149, industryCount);
            Assert.AreEqual(168, investorIndustryCount);
        }

        [TestMethod]
        public void LoadInvestorsAndLoadUsersMarkets_1_MaxValue_MaxRangeLength3_MaxRanges2_Success()
        {
            int startId = 1;
            int endId = Int32.MaxValue;
            paramParser = new QueryParameterParser(3, 2);


            sqlCommonTasks.DeleteTableData();

            RunLoadInvestorsAndLoadUsersMarkets(startId, endId);

            int investorCount;
            int industryCount;
            int investorIndustryCount;

            sqlCommonTasks.GetTableCounts(out investorCount, out industryCount, out investorIndustryCount);

            Assert.AreEqual(1, investorCount);
            Assert.AreEqual(12, industryCount);
            Assert.AreEqual(12, investorIndustryCount);
        }

        [TestMethod]
        public void LoadInvestorsAndLoadUsersMarkets_155_155_Success()
        {
            int startId = 155;
            int endId = 155;
            paramParser = new QueryParameterParser(3, 2);

            sqlCommonTasks.DeleteTableData();

            RunLoadInvestorsAndLoadUsersMarkets(startId, endId);

            int investorCount;
            int industryCount;
            int investorIndustryCount;

            sqlCommonTasks.GetTableCounts(out investorCount, out industryCount, out investorIndustryCount);

            Assert.AreEqual(1, investorCount);
            Assert.AreEqual(140, industryCount);
            Assert.AreEqual(industryCount, investorIndustryCount);
        }

        [TestMethod]
        public void LoadInvestorsAndLoadUsersMarkets_155_156_Success()
        {
            int startId = 155;
            int endId = 156;
            paramParser = new QueryParameterParser(3, 2);

            sqlCommonTasks.DeleteTableData();

            RunLoadInvestorsAndLoadUsersMarkets(startId, endId);

            int investorCount;
            int industryCount;
            int investorIndustryCount;

            sqlCommonTasks.GetTableCounts(out investorCount, out industryCount, out investorIndustryCount);

            Assert.AreEqual(1, investorCount);
            Assert.AreEqual(140, industryCount);
            Assert.AreEqual(industryCount, investorIndustryCount);
        }

        [TestMethod]
        public void LoadInvestorsAndLoadUsersMarkets_155_MaxValue_MaxRangeLength30_MaxRanges10_Success()
        {
            int startId = 155;
            int endId = Int32.MaxValue;
            paramParser = new QueryParameterParser(30,10);

            sqlCommonTasks.DeleteTableData();

            RunLoadInvestorsAndLoadUsersMarkets(startId, endId);

            int investorCount;
            int industryCount;
            int investorIndustryCount;

            sqlCommonTasks.GetTableCounts(out investorCount, out industryCount, out investorIndustryCount);

            Assert.AreEqual(126, investorCount);
            Assert.AreEqual(472, industryCount);
            Assert.AreEqual(3371, investorIndustryCount);
        }

        /// <summary>
        /// Calls LoadInvestorsAndLoadUsersMarkets as InvestorIndustriesLoader does it.
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        void RunLoadInvestorsAndLoadUsersMarkets(int startId, int endId)
        {

            var loader = new InvestorIndustriesLoader(AngelListClient, defaultLogWriter);

            // Restrict endId to the max allowed value.
            int end = paramParser.RestrictIntRange(startId, endId);

            loader.LoadInvestorsAndLoadUsersMarkets(startId, end);
        }
    }
}
