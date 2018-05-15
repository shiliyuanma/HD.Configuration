using System;
using System.Collections.Concurrent;

namespace HD.Configuration.Consul
{
    public class ChangeTokens
    {
        static ConcurrentDictionary<string, ConfigurationReloadToken> _tokens;
        static Lazy<ChangeTokens> _instance = new Lazy<ChangeTokens>(() => new ChangeTokens(), true);
        public static ChangeTokens Instance => _instance.Value;

        private ChangeTokens()
        {
            _tokens = new ConcurrentDictionary<string, ConfigurationReloadToken>();
        }

        internal ConfigurationReloadToken AddOrUpdateToken(string configKey)
        {
            return _tokens.AddOrUpdate(configKey, new ConfigurationReloadToken(), (key, oldToken) =>
            {
                return new ConfigurationReloadToken();
            });
        }

        public ConfigurationReloadToken GetToken(string configKey)
        {
            if (_tokens.TryGetValue(configKey, out ConfigurationReloadToken token))
            {
                if (token == null || token.HasChanged)
                {
                    return AddOrUpdateToken(configKey);
                }
                return token;
            }
            return null;
        }

        public void Reload(string configKey)
        {
            if (configKey == Consts.ReloadAll)
            {
                var confKeys = _tokens.Keys;
                foreach (var key in confKeys)
                {
                    var token = GetToken(key);
                    if (token != null)
                        token.OnReload();
                }
            }
            else
            {
                var token = GetToken(configKey);
                if (token != null)
                    token.OnReload();
            }
        }
    }
}
