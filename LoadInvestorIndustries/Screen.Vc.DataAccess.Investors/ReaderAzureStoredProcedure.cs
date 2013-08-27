using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
//using Screen.Vc.Interfaces;
//using Screen.Vc.Interfaces.DataAccess;
//using Screen.Vc.Model;
//using Screen.Vc.Utilities;

//using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
//using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
//using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace Screen.Vc.DataAccess.Investors
{
    public abstract class ReaderAzureStoredProcedure : AzureStoredProcedure
    {
        public ReaderAzureStoredProcedure(IConfigurationProvider configurationProvider)
            : base(configurationProvider)
        {
        }

        public override void Execute()
        {
            string connectionString = m_configurationProvider.GetConfiguration(ConfigName.SqlConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = connection.CreateCommand())
                {
                    SqlCommandState sqlCommandState = new SqlCommandState(sqlCommand);

                    SqlParameter[] parameters = new SqlParameter[1];

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = GetStoredProcedureName();

                    parameters = GetParameters();
                    sqlCommand.Parameters.AddRange(parameters);

                    // Either this, parsing returned data in the callback...
                    //var asyncResult = sqlCommand.BeginExecuteReader(new AsyncCallback(HandleCallback), sqlCommandState);

                    // ...or this, parsing it here. (Which I think was the original intent.)
                    var asyncResult = sqlCommand.BeginExecuteReader(CommandBehavior.Default);
                    
                    // Wait for async to finish. True if successful wait, false if timedout.
                    if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(m_sqlTimeoutInSeconds)))
                    {
                        // TODO: Log error message and raise event somehow.
                        throw new TimeoutException(String.Format("Timeout occured while waiting for {0} stored procedure", GetStoredProcedureName()));
                    }
                    sqlCommandState.DataReader = sqlCommand.EndExecuteReader(asyncResult);
                    ParseData(sqlCommandState.DataReader);
                }
            }

        }


        #region Protected methods. Must be implemented by concrete instances of AzureStoredProcedures

        protected abstract void ParseData(IDataReader dataReader);

        #endregion

        #region Private methods

        private void HandleCallback(IAsyncResult result)
        {
            SqlCommandState state = result.AsyncState as SqlCommandState;

            try
            {
                state.DataReader = state.SqlCommand.EndExecuteReader(result);
                ParseData(state.DataReader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Async call returned exception. See Inner Exception for details", ex);
            }
        }

        #endregion

      
    }
}
