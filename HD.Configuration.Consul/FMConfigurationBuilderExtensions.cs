using Consul;
using HD.Configuration.Abstractions;
using HD.Configuration.Consul;
using System;

namespace HD.Configuration
{
    public static class FMConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder SetConsulOptions(this IConfigurationBuilder builder, Action<ConsulClientConfiguration> configAction)
        {
            if (builder == null || configAction == null)
            {
                throw new ArgumentNullException("builder or ConsulClientConfiguration");
            }
            builder.Properties[Consts.ConsulOptionsKey] = configAction;
            return builder;
        }

        internal static Action<ConsulClientConfiguration> GetConsulOptions(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Properties.TryGetValue(Consts.ConsulOptionsKey, out object action))
            {
                return action as Action<ConsulClientConfiguration>;
            }
            return null;
        }

        public static IConfigurationBuilder AddConsulConfig(this IConfigurationBuilder builder, string configKey, bool enableReload = true)
        {
            if (builder == null || string.IsNullOrWhiteSpace(configKey))
            {
                throw new ArgumentNullException("configurationBuilder or configKey");
            }
            builder.Add(new FMConfigurationSource(configKey, enableReload));
            return builder;
        }
    }
}
