using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestClient
{
    /// <summary>
    /// Interface that clients to the AngelList REST API implement.
    /// </summary>
    /// <exception cref="RestClientException">Wraps known exceptions of implementations.</exception>
    public interface IRestClient
    {
        string Get(Uri uri);
    }
}
