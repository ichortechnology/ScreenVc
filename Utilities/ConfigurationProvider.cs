using Microsoft.WindowsAzure.ServiceRuntime;
using Screen.Vc.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Utilities
{
    public class ConfigurationProvider : IConfigurationProvider 
    {
        public ConfigurationProvider()
        {
        }

        public string GetConfiguration(string configName)
        {
            string              configValue;

            try
            {
                if (RoleEnvironment.IsAvailable)
                {
                    configValue = RoleEnvironment.GetConfigurationSettingValue(configName);
                }
                else
                {
                    configValue = ConfigurationManager.AppSettings[configName];
                }

                return configValue;
            }
            catch (RoleEnvironmentException)
            {
                
                throw;
            }
        }
    }
}
