using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using AngelList.Interfaces;
using AngelList.Query;
using AngelList.Query.Investor;
using AngelList.JsonTypes;
using Screen.Vc.DataAccess.Investors;
using LoadInvestorIndustries.Model;

namespace LoadInvestorIndustries
{
    
    public class InvestorIndustriesLoader
    {
        IAngelListClient AngelListClient { get; set; }
        LogWriter defaultLogWriter { get; set; }
        IConfigurationProvider configurationProvider;
        QueryParameterParser paramParser;

        /// <summary>
        /// Retrieves investors and their industries from AngelList and stores them in a database.
        /// </summary>
        /// <param name="angelListClient">IAngelListClient</param>
        /// <param name="logWriter">LogWriter</param>
        public InvestorIndustriesLoader(IAngelListClient angelListClient, LogWriter logWriter)
        {
            if (angelListClient == null)
            {
                throw new ArgumentNullException("angelListClient");
            }

            this.AngelListClient = angelListClient;

            if (logWriter == null)
            {
                throw new ArgumentNullException("logWriter");
            }

            this.defaultLogWriter = logWriter;

            int maxRangeLength = Properties.Settings.Default.MaxRangeLength;
            int maxRanges = Properties.Settings.Default.MaxRanges;
            this.paramParser = new QueryParameterParser(maxRangeLength, maxRanges);

            ConfigurationProvider config = new ConfigurationProvider();
            config.Add(ConfigName.SqlConnectionString, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.ScreenVcConnectionString);
            config.Add(ConfigName.SqlConnectTimeoutInSeconds, Screen.Vc.DataAccess.Investors.Properties.Settings.Default.SqlConnectTimeoutInSeconds);
            this.configurationProvider = config;
        }

        public void Load(int startId, int endId)
        {
            // Restrict endId to the max allowed value.
            int end = paramParser.RestrictIntRange(startId, endId);
            if (end != endId)
            {
                string message = string.Format("Restricting end id {0} to {1}.", endId, end);
                defaultLogWriter.Write(message);
            }

            LoadInvestorsAndLoadUsersMarkets(startId, end);
       }

        /// <summary>
        /// Given a range of AngelList user ids, retrieves investors and their industries from AngelList and stores them in a database.
        /// Range of ids is restricted by MaxRangeLength and MaxRanges.
        /// </summary>
        /// <param name="startId">AngelList user id to start the range.</param>
        /// <param name="endId">AngelList user id to end the range.</param>
        public void LoadInvestorsAndLoadUsersMarkets(int startId, int endId)
        {
            var idLists = paramParser.ParseIntRange(startId, endId);

            foreach (List<int> idList in idLists)
            {
                try
                {
                    var investorsQuery = new GetInvestorsQuery(idList, AngelListClient, defaultLogWriter);
                    List<User> result = (List<User>)investorsQuery.Execute();
                    ProcessLoadInvestorsAndLoadUsersMarkets(result);
                }
                catch (AngelListClientException ex)
                {
                    var entry = new LogEntry();
                    entry.Categories = new string[] { "General", "Warning" };
                    entry.Message = string.Format("An exception occurred when calling the service. No further ids will be processed. Id list: {0}. Exception: {1}.", String.Join(",", idList), ex);
                    entry.Severity = TraceEventType.Error;
                    defaultLogWriter.Write(entry);
                }
            }
        }

        public void ProcessLoadInvestorsAndLoadUsersMarkets(List<User> investors)
        {
            // Save investors.
            var upsertInvestor = new UpsertInvestor(investors, configurationProvider);
            upsertInvestor.Execute();

            // Make a list of ids and get the users' markets. 
            List<int> investorIdsBatch = new List<int>();
            investorIdsBatch = investors.Select(u => u.Id).ToList();

            defaultLogWriter.Write(string.Format("Found {0} investors.", investorIdsBatch.Count));
            defaultLogWriter.Write(string.Format("Investor Ids: {0}", String.Join(",",investorIdsBatch)));

            try
            {
                LoadUsersMarkets(investorIdsBatch);
            }
            catch (AngelListClientException ex)
            {
                var entry = new LogEntry();
                entry.Categories = new string[] { "General", "Warning" };
                entry.Message = string.Format("An exception occurred when calling the service. No further ids will be processed. Id list: {0}. Exception: {1}.", String.Join(",", investorIdsBatch),ex);
                entry.Severity = TraceEventType.Error;
                defaultLogWriter.Write(entry);
            }

            


        }

        public void LoadUsersMarkets(List<int> userIds)
        {
            // Get startup ids for the users.
            GetUsersStartupIdsQuery startupIdsQuery = new GetUsersStartupIdsQuery(userIds, AngelListClient);
            var userIdStartupIdsDict = startupIdsQuery.Execute();

            HashSet<int> uniqueStartupIds = new HashSet<int>();
            foreach (int userId in userIdStartupIdsDict.Keys)
            {
                foreach (int startupId in userIdStartupIdsDict[userId])
                {
                    uniqueStartupIds.Add(startupId);
                }
            }

            GetStartupsMarketsQuery startupsMarketsQuery = new GetStartupsMarketsQuery(uniqueStartupIds.ToList(), userIdStartupIdsDict, AngelListClient, defaultLogWriter);
            Dictionary<int, List<Market>> result = (Dictionary<int, List<Market>>)startupsMarketsQuery.Execute();
            ProcessStartupsMarketsQuery(result, userIdStartupIdsDict);
        }

        public void ProcessStartupsMarketsQuery(Dictionary<int, List<Market>> startupIdsMarkets, Dictionary<int, List<int>> userIdStartupIds)
        {
            // Note:  Result received might not contain all the markets of any given user.
            // AsyncState has the original list of user ids we want markets for.
            
            if (userIdStartupIds == null)
            {
                var entry = new LogEntry();
                entry.Categories = new string[] { "General", "Warning" };
                entry.Message = "ProcessStartupsMarketsQuery was null or of the wrong type.";
                entry.Severity = TraceEventType.Error;
                defaultLogWriter.Write(entry);
            }

            // To save markets to database, create a list of markets without duplicates.
            Dictionary<int, Market> marketIdMarket = new Dictionary<int, Market>();
            foreach (var markets in startupIdsMarkets.Values)
            {
                foreach (var market in markets)
                {
                    if (!marketIdMarket.ContainsKey(market.Id))
                    {
                        marketIdMarket.Add(market.Id, market);
                    }
                }
            }

            // Save markets to database.
            var upsertIndustry = new UpsertIndustry(marketIdMarket.Values.ToList(), configurationProvider);
            upsertIndustry.Execute();


            // Get a list of users and their markets, where the user's markets has no duplicates.
            Dictionary<int, HashSet<int>> userIdMarketIds = new Dictionary<int, HashSet<int>>();

            foreach (KeyValuePair<int, List<Market>> startup in startupIdsMarkets)
            {
                defaultLogWriter.Write(string.Format("Startup id: {0}", startup.Key));

                foreach (KeyValuePair<int, List<int>> userStartups in userIdStartupIds)
                {
                    if (userStartups.Value.Contains(startup.Key))
                    {
                        if (!userIdMarketIds.ContainsKey(userStartups.Key))
                        {
                            userIdMarketIds.Add(userStartups.Key, new HashSet<int>());
                        }

                        foreach (var market in startup.Value)
                        {
                            userIdMarketIds[userStartups.Key].Add(market.Id);
                        }
                    }
                }
            }

            // Get a list of user id => market id with no duplicate pairs.
            List<UserIdMarketId> userIdMarketId = new List<UserIdMarketId>();

            foreach (int userId in userIdMarketIds.Keys)
            {
                if (!userIdMarketIds.ContainsKey(userId))
                {
                    defaultLogWriter.Write(string.Format("{0} => null", userId));
                    continue;
                }
                foreach (int marketId in userIdMarketIds[userId])
                {
                    userIdMarketId.Add(new UserIdMarketId(userId, marketId));
                    defaultLogWriter.Write(string.Format("{0} => {1}", userId, marketId));
                }
            }

            // Save the user => markets relationship to db.
            var upsertInvestorIndustry = new UpsertInvestorIndustry(userIdMarketId, configurationProvider);
            upsertInvestorIndustry.Execute();

        }
    }
}
