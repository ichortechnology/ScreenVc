using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngelList
{
    /// <summary>
    /// Exception thrown by implementations of IAngelListClient, wrapping known exceptions of the implementation.
    /// </summary>
    public class AngelListClientException : Exception
    {
        public AngelListClientException() : base() { }
        public AngelListClientException(string message) : base(message) { }
        public AngelListClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
