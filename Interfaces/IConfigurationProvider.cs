using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetConfiguration(string configName);
        //ReturnType GetConfiguration<ReturnType>(string configName);
    }
}
