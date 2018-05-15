using HD.Configuration.Abstractions;
using System;
using System.Collections.Generic;

namespace HD.Configuration.Consul
{
    public class FMConfigurationProvider : ConfigurationProvider
    {
        FMConfigurationSource _source;
        RemoteConfigurationProvider _remote;
        JsonConfigurationFileParser _parse;

        public FMConfigurationProvider(FMConfigurationSource source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _remote = new RemoteConfigurationProvider(_source);
            _parse = new JsonConfigurationFileParser();

            if (_source.EnableReload)
            {
                ChangeTokens.Instance.AddOrUpdateToken(_source.ConfigKey);
                ChangeToken.OnChange(
                    () => ChangeTokens.Instance.GetToken(_source.ConfigKey),
                    () => Load());
            }
        }

        public override void Load()
        {
            var content = _remote.GetRemoteConfig();
            if (string.IsNullOrWhiteSpace(content))
            {
                Data = new Dictionary<string, string>();
            }
            else
            {
                Data = _parse.Parse(content.Trim());
            }

            //触发：ConfigurationReloadToken.OnReload() 以执行该IChangeToken注册的回调
            OnReload();
        }
    }
}
