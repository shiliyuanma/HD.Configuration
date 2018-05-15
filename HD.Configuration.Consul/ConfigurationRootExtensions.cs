using HD.Configuration.Abstractions;
using System;
using System.Linq;
using System.Threading;

namespace HD.Configuration
{
    public static class ConfigurationRootExtensions
    {
        public static string GetOracleConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "Oracle", key);
        }

        public static string GetMySqlConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "MySql", key);
        }

        public static string GetRedisConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "Redis", key);
        }

        public static string GetMongoDBConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "MongoDB", key);
        }

        public static string GetRabbitMQConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "RabbitMQ", key);
        }

        public static string GetFastDFSConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "FastDFS", key);
        }

        public static string GetSqlServerConnectionStrings(this IConfigurationRoot config, string key = "Default")
        {
            return GetConnectionStrings(config, "SqlServer", key);
        }

        public static string GetConnectionStrings(this IConfigurationRoot config, string key, string subKey = "Default")
        {
            return config[$"ConnectionStrings:{key}:{subKey}"];
        }
    }
}
