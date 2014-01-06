using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace AngelList.Query
{
    /// <summary>
    /// IAsyncResult implementation received by the AsyncCallback methods passed to AngelListPagedQuery constructors.
    /// AsyncCallback methods receiving BatchAsyncResult MUST call the SignalCompleted() method when processing of the 
    /// BatchAsyncResult.Result is completed. 
    /// </summary>
    /// <typeparam name="T">Return type of the query.</typeparam>
    public class BatchAsyncResult<T> : IAsyncResult
    {
        T result;
        public int batchCount;
        ManualResetEvent completedEvent;
        bool isCompleted;
        public static int batchCounter = 0;

        public BatchAsyncResult(T result, object state)
        {
            this.AsyncState = state;
            this.result = result;
            completedEvent = new ManualResetEvent(false);
            isCompleted = false;
            QueryBatchCounter.Increment();
        }

        public T Result
        {
            get
            {
                return result;
            }
        }

        public object AsyncState
        {
            get;
            protected set;
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return completedEvent; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return isCompleted; }
        }

        // TODO, 5, AngelList.Query: Figure out how to do call BatchAsynchResult.SignalCompleted() automatically at the end of the executing method, as AsyncResult does.
        public void SignalCompleted()
        {
            completedEvent.Set();
            isCompleted = true;
            QueryBatchCounter.Decrement();
        }

    }
}
