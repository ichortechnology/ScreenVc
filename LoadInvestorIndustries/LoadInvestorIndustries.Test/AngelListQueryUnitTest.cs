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

            IRestClient restClient = new SimpleRestClient();
            AngelListClient = new JsonNetAngelListClient(BaseAddress, restClient);
            paramParser = new QueryParameterParser(2, 3);

        }

    }
}
