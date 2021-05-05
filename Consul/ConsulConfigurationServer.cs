using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JotunShard.Configuration.Consul
{
    public class ConsulConfigurationServer : IConfigurationServer
    {
        private readonly IConfiguration _connectionConfiguration;

        public ConsulConfigurationServer(IConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        public IEnumerable<KeyValuePair<string, string>> QuerySettings()
        {
            var result = default(KVPair[]);
            var prefix = _connectionConfiguration["consul.prefix"];
            using (var client = new ConsulClient(ConfigureClient))
            {
                result = client.KV.List(prefix).Result.Response;
            }
            if (result == default(KVPair[])) yield break;
            foreach (var kvPair in result)
            {
                var key = kvPair.Key.Substring(kvPair.Key.IndexOf('/', prefix.Length) + 1);
                var value = kvPair.Value == null || kvPair.Value.Length == 0
                    ? string.Empty
                    : Encoding.UTF8.GetString(kvPair.Value);
                yield return new KeyValuePair<string, string>(key, value);
            }
        }

        private void ConfigureClient(ConsulClientConfiguration options)
        {
            if (Uri.TryCreate(_connectionConfiguration["consul.address"], UriKind.Absolute, out var clientAddress))
                options.Address = clientAddress;
            options.Datacenter = _connectionConfiguration["consul.datacenter"];
            options.Token = _connectionConfiguration["consul.token"];
            if (TimeSpan.TryParse(_connectionConfiguration["consul.waittime"], CultureInfo.InvariantCulture, out var clientWaitTime))
                options.WaitTime = clientWaitTime;
        }
    }
}