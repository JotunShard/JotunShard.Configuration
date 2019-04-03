using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace JotunShard.Configuration
{
    public class ServerConfigurationProvider : ConfigurationProvider
    {
        private readonly IConfigurationServer _server;
        private readonly StringComparer _keyComparer;

        public ServerConfigurationProvider(
            IConfigurationServerProvider serverProvider,
            IConfigurationRoot configurationRoot,
            StringComparer keyComparer = null)
        {
            _server = serverProvider.Create(configurationRoot);
            _keyComparer = keyComparer ?? StringComparer.OrdinalIgnoreCase;
            Data = new Dictionary<string, string>(_keyComparer);
        }

        public override void Load()
        {
            var newSettings = new HashSet<string>(_keyComparer);
            var oldSettings = new HashSet<string>(Data.Keys, _keyComparer);
            var changedSettings = new HashSet<string>(_keyComparer);
            var serverConfiguration = _server.QuerySettings();
            if (serverConfiguration == null) return;
            foreach (var setting in serverConfiguration)
            {
                if (!TryGet(setting.Key, out var oldValue))
                    newSettings.Add(setting.Key);
                else
                {
                    oldSettings.Remove(setting.Key);
                    if (oldValue != setting.Value)
                        changedSettings.Add(setting.Key);
                }
                Set(setting.Key, setting.Value);
            }
            foreach (var setting in oldSettings) Data.Remove(setting);
            if (newSettings.Count != 0
                || oldSettings.Count != 0
                || changedSettings.Count != 0)
                OnReload();
        }
    }
}
