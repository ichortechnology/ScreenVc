using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

using AngelList;
using AngelList.Wcf;
using AngelList.JsonNet;
using RestClient;

namespace LoadInvestorIndustries
{
    class Program
    {
        static InvestorIndustriesLoader loader;

        static void Main(string[] args)
        {
            LogWriter defaultLogWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
           
            try
            {
                // Parse command line.
                int start;
                int end;
                if (!CommandLine.TryParse(args, out start, out end))
                {
                    var entry = new LogEntry();
                    entry.Categories = new string[] {"General", "Warning" };
                    entry.Message = string.Format("Exiting. Failed to parse command line. start: {0}, end: {1}", start, end);
                    entry.Severity = TraceEventType.Critical;
                    defaultLogWriter.Write(entry);

                    throw new ArgumentException(entry.Message);
                }
                defaultLogWriter.Write(string.Format("start: {0}, end: {1}", start, end));

                // TODO,, use factory or DI to get AngelListClient implementations?
                // TODO,, use configuration for base address?
                string baseAddress = "http://api.angel.co/1";
                IRestClient restClient = new SimpleRestClient();
                IAngelListClient angelListClient = new JsonNetAngelListClient(baseAddress, restClient);
                
                //IAngelListClient angelListClient = new WcfAngelListClient();
                
                defaultLogWriter.Write(string.Format("Using AngelListClient {0}.", angelListClient.GetType()));

                loader = new InvestorIndustriesLoader(angelListClient, defaultLogWriter);

                loader.Load(start, end);
            }
            catch (Exception ex)
            {
                var entry = new LogEntry();
                entry.Categories = new string[] { "General", "Warning" };
                entry.Message = string.Format("Exiting. Unexpected exception: {0}.", ex);
                entry.Severity = TraceEventType.Critical;
                defaultLogWriter.Write(entry);

                Console.WriteLine();
                Console.WriteLine(ex);
            }

            Console.WriteLine();
            Console.WriteLine("LoadInvestorIndustries completed.");
            // TODO,, only do this when running in the debugger?
            Console.ReadLine();
        }
    }
}
