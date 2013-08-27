using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RestClient
{
    /// <summary>
    /// Simplest possible implementation of IRestClient using System.Net.HttpWebRequest.
    /// </summary>
    /// <exception cref="RestClientException"></exception>
    public class SimpleRestClient : IRestClient
    {
        // Throws RestClientException.
        public string Get(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string responseString;
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
            }
            catch (ProtocolViolationException ex)
            {
                throw new RestClientException("ProtocolViolationException", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new RestClientException("NotSupportedException", ex);
            }
            catch (WebException ex)
            {
                throw new RestClientException(ReadWebExceptionMessage(ex), ex);
            }

            return responseString;
        }

        string ReadWebExceptionMessage(WebException ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            if (ex.Response == null)
            {
                return string.Empty;
            }

            String message = null;

            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = ex.Response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream);
                message = reader.ReadToEnd();
            }
            return message;
        }
    }
}
