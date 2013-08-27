using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngelList
{
    /// <summary>
    /// Interface implemented by classes that represent an asynchronous call to the AngelList API and that return paged results in a callback.
    /// </summary>
    /// <typeparam name="TReturnType">The type of the results returned by the query.</typeparam>
    public interface IAngelListPagedQuery<TReturnType>
    {
        Type ResultType { get; }
        string QueryName { get; }
        /// <summary>
        /// Executes the query asychronously. Implementations should ensure that the IAsyncResult.AsyncWaitHandle signals only after all tasks are completed.
        /// </summary>
        /// <returns></returns>
        IAsyncResult BeginExecute();
    }
}
