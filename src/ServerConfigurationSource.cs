using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration
{
    public class ServerConfigurationSource : IConfigurationSource
    {
        public IConfigurationServerProvider ServerProvider { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ServerConfigurationProvider(ServerProvider, builder.Build());
    }
}
