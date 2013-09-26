using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

using AngelList.Interfaces;
using AngelList.JsonTypes;
using AngelList.Query;
using AngelList.Query.Investor;
using AngelList.JsonNet;
using RestClient;


namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class AngelListQueryInvestorUnitTest
    {
        public static IAngelListClient AngelListClient { get; set; }
        public static string BaseAddress { get; set; }
        public static LogWriter defaultLogWriter { get; set; }
        public static QueryParameterParser paramParser;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            defaultLogWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

            BaseAddress = "http://api.angel.co/1";
            IRestClient restClient = new SimpleRestClient();
            AngelListClient = new JsonNetAngelListClient(BaseAddress, restClient);

            paramParser = new QueryParameterParser(2, 3);

        }

        [TestMethod]
        public void GetUsersStartupIdsQuery_Success()
        {
            var ids = new List<int> { 155, 160 }; // investor:true

            var query = new GetUsersStartupIdsQuery(ids, AngelListClient);
            Dictionary<int, List<int>> usersStartupIds = query.Execute();

            Assert.IsNotNull(usersStartupIds);
            Assert.AreEqual(ids.Count, usersStartupIds.Keys.Count);

            foreach (var startupIds in usersStartupIds.Values)
            {
                Assert.IsNotNull(startupIds);
                Assert.AreNotEqual(0, startupIds.Count);
            }
        }

        [TestMethod]
        public void GetInvestorsQuery_Success()
        {
            int startId = 155; // investor:true
            int endId = 156; // investor:false

            var idLists = paramParser.ParseIntRange(startId, endId);

            foreach (List<int> idList in idLists)
            {
                var investorsQuery = new GetInvestorsQuery(idList, new AsyncCallback(GetInvestors_Success_ProcessResult), AngelListClient, defaultLogWriter);

                var asyncResult = investorsQuery.BeginExecute();
                asyncResult.AsyncWaitHandle.WaitOne(20 * 1000);
            }
        }

        public void GetInvestors_Success_ProcessResult(IAsyncResult iaResult)
        {
            var aresult = (BatchAsyncResult<List<User>>)iaResult;
            List<User> investors = aresult.Result;

            int startId = 155; // investor:true
            int endId = 156; // investor:false

            Assert.IsNotNull(investors);
            Assert.AreNotEqual(0, investors.Where<User>(u => u.Id == startId).Count<User>());
            Assert.AreEqual(0, investors.Where<User>(u => u.Id == endId).Count<User>());
        }

        [TestMethod]
        public void GetStartupsMarketsQuery_Success()
        {
            string startupIdsString = "13779,153114,54476,202927,228506,52745,168275,6702";
            string[] startupIdsStrings = startupIdsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] startupIds = Array.ConvertAll<string, int>(startupIdsStrings, new Converter<string, int>(Convert.ToInt32));

            // Get markets for this batch of users.
            var startupsMarketsCallback = new AsyncCallback(ProcessStartupsMarketsQuery);

            GetStartupsMarketsQuery startupsMarketsQuery = new GetStartupsMarketsQuery(startupIds.ToList(), startupsMarketsCallback, AngelListClient, defaultLogWriter);

            IAsyncResult result = startupsMarketsQuery.BeginExecute();

            if (!result.AsyncWaitHandle.WaitOne(20 * 1000))
            {
                throw new TimeoutException("GetStartupsMarketsQuery");
            }
        }

        public void ProcessStartupsMarketsQuery(IAsyncResult iaResult)
        {
            var aresult = (BatchAsyncResult<Dictionary<int, List<Market>>>)iaResult;
            Dictionary<int, List<Market>> startupIdsMarkets = aresult.Result;
            // Note:  Result received might not contain all the markets of any given user.

            // AsyncState has the original list of user ids we want markets for.
            var userIdStartupIds = aresult.AsyncState as Dictionary<int, List<int>>;

            Assert.IsNotNull(startupIdsMarkets);

            // Signal that processing of the IAsyncResult is completed.
            aresult.SignalCompleted();
        }

        [TestMethod]
        public void GetStartupsOfUsersBatch_Success()
        {
            var ids = new List<int> { 155, 160 }; // investor:true
            IAngelListClient angelListClient = AngelListClient;

            var idLists = paramParser.ParseIntList(ids);

            foreach (List<int> idList in idLists)
            {
                var query = new GetUsersStartupIdsQuery(idList, angelListClient);
                Dictionary<int, List<int>> usersStartupIds = query.Execute();

                HashSet<int> uniqueStartupIds = new HashSet<int>();
                foreach (List<int> startupIds in usersStartupIds.Values)
                {
                    foreach (int startupId in startupIds)
                    {
                        uniqueStartupIds.Add(startupId);
                    }
                }

                string startupIdsString = String.Join(",", uniqueStartupIds);

                var startupsBatchCallback = new AsyncCallback(GetStartupsBatch_Success_ProcessResults);

                var startupsBatchQuery = new GetStartupsBatchQuery(uniqueStartupIds.ToList(), startupsBatchCallback, angelListClient, defaultLogWriter);

                var result = startupsBatchQuery.BeginExecute();
                result.AsyncWaitHandle.WaitOne(20 * 1000);
            }
        }

        public void GetStartupsBatch_Success_ProcessResults(IAsyncResult iaResult)
        {
            var aresult = (BatchAsyncResult<List<AngelList.JsonTypes.Startup>>)iaResult;
            List<AngelList.JsonTypes.Startup> startups = aresult.Result;

            IAngelListClient angelListClient = AngelListClient;

            Assert.IsNotNull(startups);
            Assert.AreNotEqual(0, startups.Count);

            // Expect one startup for each id, so no more startups returned than the number of ids sent in a query.
            Assert.IsTrue(startups.Count <= angelListClient.QueryParameterLimit);
        }
    }
}
