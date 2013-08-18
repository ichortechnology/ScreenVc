using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using Screen.Vc.Interfaces;
using Screen.Vc.Interfaces.DataAccess;
using Screen.Vc.Model;
using Screen.Vc.Utilities;

using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace Screen.Vc.DataAccess
{
    public abstract class AzureStoredProcedure : IAzureStoredProcedure
    {
        public AzureStoredProcedure(IConfigurationProvider configurationProvider, SqlConnection dbConnection)
        {
            this.m_configurationProvider = configurationProvider;
            m_sqlTimeoutInSeconds = Convert.ToInt32(m_configurationProvider.GetConfiguration(ConfigName.SqlConnectTimeoutInSeconds));
            m_dbConnection = dbConnection;
        }

        public void Execute()
        {
            string          connectionString = m_configurationProvider.GetConfiguration("SqlConnectionString");
            
            m_dbConnection.ConnectionString = connectionString;
            using (m_dbConnection)
            {
                m_dbConnection.Open();
                using (var sqlCommand = m_dbConnection.CreateCommand())
                { 
                    SqlCommandState             sqlCommandState = new SqlCommandState(sqlCommand);
                    SqlParameter[]              parameters = new SqlParameter[1];

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = GetStoredProcedureName();

                    parameters = GetParameters();
                    sqlCommand.Parameters.AddRange(parameters);
                    var sqlDataReader = sqlCommand.ExecuteReader();
/*                    var asyncResult = sqlCommand.BeginExecuteReader(new AsyncCallback(HandleCallback), sqlCommandState);

                    // Wait for async to finish. True if successful wait, false if timedout.
                    if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(m_sqlTimeoutInSeconds)))
                    {
                        // TODO: Log error message and raise event somehow.
                        throw new TimeoutException(String.Format("Timeout occured while waiting for {0} stored procedure",
                                                                 StoredProcedureName.EntrepreneurHomePageDataGet));
                    }
                    if (sqlCommandState.Exception != null)
                    {
                        throw new ApplicationException("Async call returned exception. See Inner Exception for details", sqlCommandState.Exception);
                    }
 */
                    ParseData(sqlDataReader);
                }
            }

        }


        #region Protected methods. Must be implemented by concrete instances of AzureStoredProcedures

        protected abstract string GetStoredProcedureName();

        protected abstract SqlParameter[] GetParameters();

        protected abstract void ParseData(SqlDataReader dataReader);


        #endregion

        #region Private methods

        private void HandleCallback(IAsyncResult result)
        {
            SqlCommandState        state = result.AsyncState as SqlCommandState;

            try
            {
                state.DataReader = state.SqlCommand.EndExecuteReader(result);
                state.SignalWaitState();
            }
            catch (Exception e)
            {
                state.Exception = e;
            }
        }

        #endregion

        #region Private member variables

        private IConfigurationProvider                      m_configurationProvider;
        private Int32                                       m_sqlTimeoutInSeconds;
        private SqlConnection                               m_dbConnection;

        #endregion Private member variables

    }
}
