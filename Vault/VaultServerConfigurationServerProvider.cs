using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration.Vault
{
    public class VaultServerConfigurationServerProvider : IConfigurationServerProvider
    {
        public IConfigurationServer Create(IConfiguration connectionConfiguration)
            => new VaultServerConfigurationServer(connectionConfiguration);
    }
}