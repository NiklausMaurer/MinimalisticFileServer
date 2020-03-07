using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace MinimalisticFileServerTest
{
    public class ConfigurationDummy : IConfiguration
    {
        private Dictionary<string, string> ConfigurationDict { get; }

        public ConfigurationDummy(Dictionary<string, string> configurationDict)
        {
            ConfigurationDict = configurationDict;
        }
        
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }

        public string this[string key]
        {
            get => this.ConfigurationDict[key];
            set => throw new System.NotImplementedException();
        }
    }
}