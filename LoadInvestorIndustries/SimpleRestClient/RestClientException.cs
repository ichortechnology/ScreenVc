using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestClient
{
    /// <summary>
    /// Exception thrown by implementations of IRestClient, wrapping known exceptions of the implementation.
    /// </summary>
    public class RestClientException : Exception
    {
        public RestClientException() : base() { }
        public RestClientException(string message) : base(message) { }
        public RestClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
