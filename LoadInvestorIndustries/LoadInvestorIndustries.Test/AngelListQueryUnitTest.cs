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

using AngelList;
using AngelList.JsonTypes;
using AngelList.JsonTypes.UserJsonTypes;
using AngelList.JsonTypes.UserRoleJsonTypes;
using AngelList.JsonTypes.StartupJsonTypes;
using AngelList.Query;
using AngelList.Query.Investor;
using AngelList.Wcf;
using AngelList.JsonNet;
using RestClient;


namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class AngelListUnitTest
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
            //AngelListClient = new WcfAngelListClient();

            IRestClient restClient = new SimpleRestClient();
            AngelListClient = new JsonNetAngelListClient(BaseAddress, restClient);
            paramParser = new QueryParameterParser(2, 3);

        }

        [TestMethod]
        public void GetUsersBatchQuery_Success()
        {
            // TODO,, test (155 160) forexample
            Assert.Fail("Not Implemented");
        }

        [TestMethod]
        public void GetUserRolesQuery_Success()
        {
            // TODO,, test 155 and 156
            Assert.Fail("Not Implemented");
        }

        [TestMethod]
        public void GetStartupsBatchQuery_Success()
        {
            // Startup.Id for users 155 and 160
            //"13779,153114,54476,202927,228506,52745,168275,6702,214291,21180,30577,20848,170848,170451,189038,151433,37846,16896,688,113598,96497,54330,97116,94949,132725,93098,122276,76047,25312,36853,19563,16828,50317,32545,80936,967,77992,592,16467,26302,55100,35849,39161,32187,1347,32441,2465,19147,19154,32934,36184,2436,60416,32469,60418,44627,23420,65735,57028,2674,13365,6343,7601,1420,21272,13302,20026,19580,23120,15170,20383,20944,19435,13152,18872,22317,19607,7824,37685,32728,32641,32630,32158,38493,32531,32355,34101,32566,19163,32143,19169,16570,32510,32436,32303,32240,16775,29999,18978,28756,19393,16447,27572,24469,19150,19168,12387,19155,21902,3284,4064,19897,19165,1085,1043,17068,29321,3678,3295,2233,516,433,32969,77935,36120,101592,1618,36121,32107,31223,18863,787,4551,3464,1963,2079,1410"
            // TODO,, Test some of these.
            Assert.Fail("Not Implemented");
        }

    }
}
