using System;
using System.Collections.Generic;

using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ninject;
using Ninject.MockingKernel.Moq;

using Screen.Vc.DataAccess;
using Screen.Vc.Interfaces;
using Screen.Vc.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.UnitTests
{
    public static class KernelBinding
    {
        public static void Initialize(MoqMockingKernel kernel)
        {
            SetupConfiguration(kernel);
            BindExponentialBackoffStrategy(kernel);
            BindRetryManager(kernel);

            kernel.Bind<EntrepreneurHomePageData>().ToSelf();
        }

        private static void SetupConfiguration(MoqMockingKernel kernel)
        {
            var configProvider = kernel.GetMock<IConfigurationProvider>();
            configProvider.Setup(m => m.GetConfiguration(ConfigName.SqlConnectionString)).Returns("Server=localhost; Database=ScreenVc; Trusted_Connection=True");
            configProvider.Setup(m => m.GetConfiguration(ConfigName.RetryCount)).Returns("2");
            configProvider.Setup(m => m.GetConfiguration(ConfigName.SqlConnectMinBackOffInSeconds)).Returns("10");
            configProvider.Setup(m => m.GetConfiguration(ConfigName.SqlConnectMaxBackOffInSeconds)).Returns("60");
            configProvider.Setup(m => m.GetConfiguration(ConfigName.SqlConnectDeltaBackOffInSeconds)).Returns("10");
            configProvider.Setup(m => m.GetConfiguration(ConfigName.SqlConnectTimeoutInSeconds)).Returns("120");
        }

        private static void BindExponentialBackoffStrategy(MoqMockingKernel kernel)
        {
            var configuration = kernel.Get<IConfigurationProvider>();
            var retryCount = Convert.ToInt32(configuration.GetConfiguration(ConfigName.RetryCount));
            var minBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectMinBackOffInSeconds)));
            var maxBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectMaxBackOffInSeconds)));
            var deltaBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectDeltaBackOffInSeconds)));


            var expBackoff = new ExponentialBackoff("ExponentialBackoff", 10, minBackoff, maxBackoff, deltaBackoff);
            
            kernel.Bind<ExponentialBackoff>().ToConstant<ExponentialBackoff>(expBackoff).Named("ExponentialBackoff");
            kernel.Bind<RetryStrategy>().ToConstant<ExponentialBackoff>(expBackoff).Named("ExponentialBackoff");
        }

        private static void BindRetryManager(MoqMockingKernel kernel)
        {
            string      retryStrategyName = "ExponentialBackoff";
            var retryStrategy = kernel.Get<RetryStrategy>(retryStrategyName);

            var retryStrategies = new List<RetryStrategy>() { kernel.Get<ExponentialBackoff>() };

            kernel.Bind< IEnumerable<RetryStrategy> >().ToConstant< List<RetryStrategy> >(retryStrategies).Named("RetryManagerStrategies");
            var retryStrategyNameMap = new Dictionary<string, string>();
            retryStrategyNameMap.Add(retryStrategyName, retryStrategyName);

            var retryManager = new CustomRetryManager(retryStrategies, retryStrategyName, retryStrategyNameMap);
            kernel.Bind<CustomRetryManager>().ToConstant(retryManager);
            RetryManager.SetDefault(retryManager);
        }

    }
}
