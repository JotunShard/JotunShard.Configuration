using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration.Consul
{
    public class ConsulConfigurationServerProvider : IConfigurationServerProvider
    {
        public IConfigurationServer Create(IConfiguration connectionConfiguration)
            => new ConsulConfigurationServer(connectionConfiguration);
    }
}