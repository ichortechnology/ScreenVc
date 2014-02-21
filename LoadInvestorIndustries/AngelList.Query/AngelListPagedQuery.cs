using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using AngelList.Interfaces;

namespace AngelList.Query
{
    /// <summary>
    /// Abstract base class for implementations of IAngelListPagedQuery. 
    /// </summary>
    /// <typeparam name="TReturnType">Return type of the query.</typeparam>
    public abstract class AngelListPagedQuery<TReturnType> : IAngelListPagedQuery<TReturnType>
    {
        protected delegate void ExecuteInvoker();
        protected IAsyncResult ExecuteInvokerAsyncResult;
        protected List<BatchAsyncResult<TReturnType>> batchResults = new List<BatchAsyncResult<TReturnType>>();
        protected object State { get; set; }
        protected LogWriter defaultLogWriter;

        public string QueryName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public Type ResultType
        {
            get { return typeof(TReturnType); }
        }

        protected IAngelListClient AngelListClient { get; private set; }

        public AngelListPagedQuery(IAngelListClient angelListClient, LogWriter logWriter)
            : this(null, angelListClient, logWriter)
        {
        }

        public AngelListPagedQuery(object state, IAngelListClient angelListClient, LogWriter logWriter)
        {
            if (logWriter == null)
            {
                throw new ArgumentNullException("logWriter");
            }
            
            if (angelListClient == null)
            {
                throw new ArgumentNullException("angelListClient");
            }

            this.defaultLogWriter = logWriter;
            this.AngelListClient = angelListClient;
            this.State = state;
        }

        /// <summary>
        /// Performs the asynchronous query and returns an IAsyncResult that will be signalled when all of the query results are processed.
        /// </summary>
        /// <returns>An IAsyncResult, the IAsyncResult.AsychWaitHandle of which is signalled when all results are processed.</returns>
        public IAsyncResult BeginExecute()
        {
            ExecuteInvoker invoker = new ExecuteInvoker(this.ExecuteAndWaitForBatches);
            ExecuteInvokerAsyncResult = invoker.BeginInvoke(new AsyncCallback(EndExecute), null);
            return ExecuteInvokerAsyncResult;
        }

        void EndExecute(IAsyncResult iaResult)
        {
            AsyncResult aresult = (AsyncResult)iaResult;
            ExecuteInvoker caller = (ExecuteInvoker)aresult.AsyncDelegate;
            if (!aresult.EndInvokeCalled)
            {
                caller.EndInvoke(iaResult);
            }
        }

        void ExecuteAndWaitForBatches()
        {
            Execute();

            if (this.batchResults == null || batchResults.Count == 0)
            {
                return;
            }

            WaitHandle[] handles = batchResults.Select(b => b.AsyncWaitHandle).ToArray();
            WaitHandle.WaitAll(handles);
        }

        /// <summary>
        /// Method that implementations override to perform a query. Results of the query should be given as a parameter to the method CallBatchCallback.
        /// </summary>
        abstract public Object Execute();

        /// <summary>
        /// Used by implementations to return the results of the query. Sends the results to the AsyncCallback that processes the results
        /// and ensures that the IAsyncResult returned by BeginExecute() is signalled only when all results are processed.
        /// </summary>
        /// <param name="batch">The batch of results to be processed by the callback. The callback should expect to handle multiple calls from paged query results.</param>
        protected void CallBatchCallback(TReturnType batch)
        {
            var result = new BatchAsyncResult<TReturnType>(batch, this.State);
            batchResults.Add(result);
        }
    }
}
