using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace AngelList.Query
{
    public class QueryBatchCounter
    {
        static int batchCounter;
        static ManualResetEvent resetEvent = new ManualResetEvent(false);

        public static int Count { get { return batchCounter; } }

        public static void Increment()
        {
            Interlocked.Increment(ref batchCounter);
            resetEvent.Reset();
        }

        public static void Decrement()
        {
            Interlocked.Decrement(ref batchCounter);
            if (Count == 0)
            {
                resetEvent.Set();
            }
        }

        public static WaitHandle WaitHandle { get { return resetEvent; } }
    }
}
