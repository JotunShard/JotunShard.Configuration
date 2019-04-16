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
            IConfigurationServer server,
            StringComparer keyComparer = null)
        {
            _server = server;
            _keyComparer = keyComparer ?? StringComparer.OrdinalIgnoreCase;
            Data = new Dictionary<string, string>(_keyComparer);
        }

        public override void Load()
        {
            var hasNewSettings = false;
            var oldSettings = new HashSet<string>(Data.Keys, _keyComparer);
            var hasChangedSettings = false;
            var serverConfiguration = _server.QuerySettings();
            if (serverConfiguration == null) return;
            foreach (var setting in serverConfiguration)
            {
                if (!TryGet(setting.Key, out var oldValue))
                    hasNewSettings = true;
                else
                {
                    oldSettings.Remove(setting.Key);
                    if (oldValue != setting.Value)
                        hasChangedSettings = true;
                }
                Set(setting.Key, setting.Value);
            }
            foreach (var setting in oldSettings) Data.Remove(setting);
            if (hasNewSettings
                || oldSettings.Count != 0
                || hasChangedSettings)
                OnReload();
        }
    }
}