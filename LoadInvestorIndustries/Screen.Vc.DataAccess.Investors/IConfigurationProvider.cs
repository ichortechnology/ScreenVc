using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Screen.Vc.DataAccess.Investors
{
    public interface IConfigurationProvider
    {
        string GetConfiguration(string key);
        T GetConfiguration<T>(string key);
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        Dictionary<string, object> config;


        public ConfigurationProvider()
        {
            this.config = new Dictionary<string, object>();
        }

        public string GetConfiguration(string key)
        {
            return GetConfiguration<string>(key);
        }

        public T GetConfiguration<T>(string key) 
        {
            object configObject;
            if (!config.TryGetValue(key, out configObject))
            {
                throw new ConfigurationErrorsException(string.Format("Configuration key {0} not found.", key));
            }

            if (!(configObject is T))
            {
                throw new ConfigurationErrorsException(string.Format("Configuration value of type {1} associated with key {0} not of requested type {2}.", key, configObject.GetType(), typeof(T)));
            }

            return (T)configObject;
        }

        public void Add(string key, object value)
        {
            config.Add(key, value);
        }
    }

    public static class ConfigName
    {
        public const string SqlConnectTimeoutInSeconds = "SqlConnectTimeoutInSeconds";
        public const string SqlConnectionString = "SqlConnectionString";
    }
}
