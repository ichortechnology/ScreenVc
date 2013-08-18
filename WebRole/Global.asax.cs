using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Screen.Vc.Utilities;
using Screen.Vc.Interfaces;
using Screen.Vc.DataAccess;
using Screen.Vc.Interfaces.DataAccess;

namespace Screen.Vc.WebRole
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication  //System.Web.HttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            Application.Add("NinjectKernel", kernel);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private void RegisterServices(IKernel kernel)
        {
            // Sequence of these bindings is important. Otherwise, things will not be registered correctly.

            kernel.Bind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();

            BindExponentialBackoffStrategy(kernel);
            BindRetryManager(kernel);

            kernel.Bind<RetryStrategy>().To<RetryStrategy>();
            kernel.Bind<RetryManager>().To<CustomRetryManager>().InSingletonScope();
            kernel.Bind<CustomRetryManager>().To<CustomRetryManager>().InSingletonScope();
            kernel.Bind<IEntrepreneurHomePageData>().To<EntrepreneurHomePageData>();
            // Initialize RetryManager default instance.
            kernel.TryGet<RetryManager>();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        #region Bindings

        private void BindExponentialBackoffStrategy(IKernel kernel)
        {
            var configuration = kernel.Get<IConfigurationProvider>();
            var retryCount = Convert.ToInt32(configuration.GetConfiguration(ConfigName.RetryCount));
            var minBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectMinBackOffInSeconds)));
            var maxBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectMaxBackOffInSeconds)));
            var deltaBackoff = new TimeSpan(0, 0, Convert.ToInt32(configuration.GetConfiguration(ConfigName.SqlConnectDeltaBackOffInSeconds)));

            var backOff = new ExponentialBackoff("ExponentialBackoff", retryCount, minBackoff, maxBackoff, deltaBackoff, true);
            kernel.Bind<RetryStrategy>().ToConstant<RetryStrategy>(backOff).Named("ExponentialBackoff");
            kernel.Bind<ExponentialBackoff>().ToConstant<ExponentialBackoff>(backOff);
        }

        private void BindRetryManager(IKernel kernel)
        {
            string      retryStrategyName = "ExponentialBackoff";
            var retryStrategy = kernel.Get<RetryStrategy>(retryStrategyName);
            var retryStrategies = new List<RetryStrategy>() { kernel.Get<ExponentialBackoff>() };

            kernel.Bind< IEnumerable<RetryStrategy> >().ToConstant< List<RetryStrategy> >(retryStrategies).Named("RetryManagerStrategies");
            
            var retryManager = new CustomRetryManager(retryStrategies, retryStrategyName, null);
            kernel.Bind<CustomRetryManager>().ToConstant(retryManager);
            RetryManager.SetDefault(retryManager);
        }

        #endregion Bindings
    }
}