using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Interfaces
{
    public interface IIocContainer : IDisposable
    {
        IConfigurationProvider ConfigurationProvider { get; set; }
        ILogProvider LogProvider { get; set; }
    }
}
