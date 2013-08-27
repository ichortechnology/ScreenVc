using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.DataAccess.Investors
{
    class SqlCommandState 
    {
        ManualResetEvent resetEvent;

        WaitHandle WaitHandle { get { return resetEvent; } }

        public SqlCommand SqlCommand { get; set; }

        public SqlDataReader DataReader { get; set; }

        public int RowsAffected { get; set; }

        public Exception Exception { get; set; }

        public SqlCommandState(SqlCommand sqlCommand)
        {
            this.SqlCommand = sqlCommand;
            resetEvent = new ManualResetEvent(false);
        }

        public void SignalWaitState()
        {
            resetEvent.Set();
        }
    }
}
