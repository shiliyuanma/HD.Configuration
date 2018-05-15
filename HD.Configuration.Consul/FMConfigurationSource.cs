using HD.Configuration.Abstractions;
using HD.Configuration;
using System;
using Consul;

namespace HD.Configuration.Consul
{
    public class FMConfigurationSource : IConfigurationSource
    {
        public Action<ConsulClientConfiguration> ConsulConfigAction { get; set; }

        public string ConfigKey { get; set; }

        public bool EnableReload { get; set; }

        public FMConfigurationSource(string configKey, bool enableReload)
        {
            ConfigKey = configKey.Trim();
            EnableReload = enableReload;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            ConsulConfigAction = builder.GetConsulOptions();
            if (ConsulConfigAction == null)
            {
                throw new ArgumentNullException("ConsulConfigAction");
            }
            return new FMConfigurationProvider(this);
        }
    }
}
