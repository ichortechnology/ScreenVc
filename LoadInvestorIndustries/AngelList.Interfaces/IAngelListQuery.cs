using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngelList.Interfaces
{
    /// <summary>
    /// Interface implemented by classes that represent a synchronous call to the AngelList API and that don't return paged results in a callback.
    /// For example, a query that returns all Startup ids for a user, and that returns paged results in a single list.
    /// </summary>
    /// <typeparam name="TReturnType">The type of the results returned by the query.</typeparam>
    public interface IAngelListQuery<TReturnType>
    {
        Type ResultType { get; }
        TReturnType Result { get; }
        string QueryName { get; }
        TReturnType Execute();
    }
}
