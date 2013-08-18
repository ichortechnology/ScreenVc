using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Interfaces.Exceptions
{
    /// <summary>
    /// Base type used to create all exceptions by ScreenVc application.
    /// </summary>
    public class ScreenVcException : ApplicationException
    {
        public ScreenVcException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
