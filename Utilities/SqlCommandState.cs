using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Screen.Vc.Utilities
{
    public class SqlCommandState : IAsyncResult
    {
        public SqlCommandState(SqlCommand sqlCommand)
        {
            this.SqlCommand = sqlCommand;
            this.Exception = null;
        }

        public void SignalWaitState()
        {
            m_asyncWaitHandle.Set();
        }

        public object AsyncState
        {
            get { return this; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return m_asyncWaitHandle; }
        }

        public bool CompletedSynchronously { get; set; }

        public bool IsCompleted { get; set; }

        public SqlCommand SqlCommand { get; set; }

        public SqlDataReader DataReader { get; set; }

        public Exception Exception { get; set; }
        #region Private member variables

        private AutoResetEvent              m_asyncWaitHandle = new AutoResetEvent(false);

        #endregion
    }
}
