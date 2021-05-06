using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Vault;

namespace JotunShard.Configuration.Vault
{
    internal class VaultServerConfigurationServer : IConfigurationServer
    {
        private readonly IConfiguration _connectionConfiguration;

        public VaultServerConfigurationServer(IConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        public IEnumerable<KeyValuePair<string, string>> QuerySettings()
        {
            var prefix = _connectionConfiguration["vault.prefix"];
            var client = new VaultClient(ConfigureClient());
            var result = client.Secret.Read<Dictionary<string, string>>(prefix).Result;
            return result?.Data ?? Enumerable.Empty<KeyValuePair<string, string>>();
            //var result = client.Secret.List(prefix).Result;
            //if (result == null) yield break;
            //foreach (var dataKey in result.Data.Keys)
            //{
            //    result.Data.
            //}
        }

        private VaultOptions ConfigureClient()
        {
            var options = new VaultOptions();
            if (Uri.TryCreate(_connectionConfiguration["vault.address"], UriKind.Absolute, out var clientAddress))
                options.Address = clientAddress.ToString();
            options.Token = _connectionConfiguration["vault.token"];
            return options;
        }
    }
}