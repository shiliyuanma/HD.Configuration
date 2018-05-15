using Consul;
using System.Text;

namespace HD.Configuration.Consul
{
    internal class RemoteConfigurationProvider
    {
        FMConfigurationSource _source;
        public RemoteConfigurationProvider(FMConfigurationSource source)
        {
            _source = source;
        }
        /// <summary>
        /// 从consul获取json配置
        /// </summary>
        /// <returns>
        /// json字符串
        /// </returns>
        public string GetRemoteConfig()
        {
            using (var client = new ConsulClient(_source.ConsulConfigAction))
            {
                var getPair = client.KV.Get(_source.ConfigKey).Result;
                return Encoding.UTF8.GetString(getPair.Response.Value, 0, getPair.Response.Value.Length);
            }
        }
    }
}
