using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

//using Screen.Vc.Interfaces;
//using Screen.Vc.Interfaces.DataAccess;
//using Screen.Vc.Model;
//using Screen.Vc.Utilities;

//using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
//using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
//using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace Screen.Vc.DataAccess.Investors
{
    public abstract class AzureStoredProcedure : IAzureStoredProcedure
    {
        protected IConfigurationProvider m_configurationProvider;
        protected Int32 m_sqlTimeoutInSeconds;

        public AzureStoredProcedure(IConfigurationProvider configurationProvider)
        {
            this.m_configurationProvider = configurationProvider;
            m_sqlTimeoutInSeconds = m_configurationProvider.GetConfiguration<int>(ConfigName.SqlConnectTimeoutInSeconds);
        }

        public abstract void Execute();

        #region Protected methods. Must be implemented by concrete instances of AzureStoredProcedures

        protected abstract string GetStoredProcedureName();

        protected abstract SqlParameter[] GetParameters();

        #endregion

    
    }
}
