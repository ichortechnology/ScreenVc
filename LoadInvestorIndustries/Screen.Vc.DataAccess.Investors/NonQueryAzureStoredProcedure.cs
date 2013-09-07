using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public abstract class NonQueryAzureStoredProcedure : AzureStoredProcedure
    {
        public NonQueryAzureStoredProcedure(IConfigurationProvider configurationProvider)
            : base(configurationProvider)
        {
        }

        public override void Execute()
        {
            string connectionString = m_configurationProvider.GetConfiguration(ConfigName.SqlConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var sqlCommand = connection.CreateCommand())
                {
                    SqlCommandState sqlCommandState = new SqlCommandState(sqlCommand);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = GetStoredProcedureName();

                    var parameters = GetParameters();
                    sqlCommand.Parameters.AddRange(parameters);

                    var asyncResult = sqlCommand.BeginExecuteNonQuery();

                    if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(m_sqlTimeoutInSeconds)))
                    {
                        throw new TimeoutException(String.Format("Timeout occured while waiting for {0} stored procedure", GetStoredProcedureName()));
                    }
                    int rowsAffected = sqlCommand.EndExecuteNonQuery(asyncResult);

                    CheckNonQueryReturnValue(rowsAffected);
                }
            }
        }

        protected abstract void CheckNonQueryReturnValue(int rowsAffected);
    }
}
